using Caliph.Library.Models;
using Caliph.API.Helper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Caliph.API.Handlers
{
    public class TokenHandler : DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            IEnumerable<string> authzHeaders;
            try
            {
                token = null;
                if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
                {
                    return false;
                }
                var bearerToken = authzHeaders.ElementAt(0);
                if (!bearerToken.StartsWith("Bearer "))
                    return false;
                token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;

                return true;
            }
            catch (Exception)
            {
                token = "";
                return false;
            }
        }

        /// <summary>
        /// Inner handler for token authentication
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;
            string message;
            //determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                //allow requests with no token - whether a action method needs an authentication can be set with the claimsauthorization attribute
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var principal = JwtManager.GetPrincipal(token);
                if (principal != null)
                {
                    //extract and assign the user of the jwt
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principal;
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException e)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = e.Message;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage
            {
                StatusCode = statusCode,
                RequestMessage = request,
                Content = new ObjectContent(typeof(ErrResponse), new ErrResponse { StatusMsg = message }, new JsonMediaTypeFormatter())
            });
        }
    }
}