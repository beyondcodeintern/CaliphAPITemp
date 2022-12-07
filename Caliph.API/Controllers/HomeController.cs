using Caliph.API.Helper;
using Caliph.API.Models;
using Caliph.Library.Helper;
using NLog;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Caliph.API.Controllers
{
    [RoutePrefix("api")]
    public class HomeController : ApiController
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// API to get authentication token using username and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetToken")]
        [AllowAnonymous]
        public IHttpActionResult GetToken([FromBody] LoginRequest request)
        {
            string functionParam = "";
            try
            {
                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam = new JavaScriptSerializer().Serialize(request);
                #endregion

                if (request.Username.Equals(ConfigHelper.token_username) && request.Password.Equals(ConfigHelper.token_pw))
                {
                    var token = JwtManager.GenerateToken(request.Username);

                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                logger.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
                return Unauthorized();
            }

            return Unauthorized();
        }
    }
}
