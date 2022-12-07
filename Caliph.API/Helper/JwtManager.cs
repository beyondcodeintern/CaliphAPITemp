using Caliph.Library.Helper;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace Caliph.API.Helper
{
    public class JwtManager
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly string secret = ConfigHelper.secret;
        private static readonly string issuer = ConfigHelper.issuer;
        private static readonly string audience = ConfigHelper.audience;

        /// <summary>
        /// Method to generate new token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GenerateToken(string code)
        {
            try
            {
                var symmetricKey = Convert.FromBase64String(secret);
                var tokenHandler = new JwtSecurityTokenHandler();

                var now = DateTime.UtcNow;

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new[]
                        {
                        new Claim(ClaimTypes.Name, code)
                        }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = issuer,
                    Audience = audience,
                    IssuedAt = now,
                    Expires = now.AddSeconds(ConfigHelper.TokenExpiryInSecond)
                };
                var stoken = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(stoken);
            }
            catch (Exception ex)
            {
                logger.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, $"code={code}", ex.ToString()));
            }

            return "";
        }

        /// <summary>
        /// Method to get principal from JWT 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(secret);

                var validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateLifetime = true
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                var identity = principal?.Identity as ClaimsIdentity;

                if (identity == null)
                    return null;

                if (!identity.IsAuthenticated)
                    return null;

                return principal;
            }
            catch (Exception ex)
            {
                logger.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, $"token={token}", ex.ToString()));
            }

            return null;
        }
    }
}