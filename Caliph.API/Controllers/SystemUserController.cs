using Caliph.API.Models;
using Caliph.Library;
using Caliph.Library.Helper;
using Caliph.Library.Models;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Caliph.API.Controllers
{
    [RoutePrefix("api/v1/system-user")]
    public class SystemUserController : BaseController
    {
        /// <summary>
        /// Get Master Datas by Master Id
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("get")]
        public IHttpActionResult GetSystemUserByUsername([FromBody] SystemUserRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.PW))
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oUsersEnt = oCaliphService.GetSystemUserByUsername(request);
                if (oUsersEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_SYSTEM_USER_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_SYSTEM_USER_MSG;
                    return Ok(response);
                }

                if (!oUsersEnt.PW.Equals(request.PW))
                {
                    response.StatusCode = APIStatusCode.INCORRECT_PW_CODE;
                    response.StatusMsg = APIStatusCode.INCORRECT_PW_MSG;
                    return Ok(response);
                }

                oUsersEnt.PW = ""; // clear password return fields
                oUsersEnt.MenuList = oCaliphService.GetMenuByUser(request.Username);

                response.data = oUsersEnt;
                response.StatusCode = APIStatusCode.SUCC_CODE;
                response.StatusMsg = APIStatusCode.SUCC_MSG;

                oCaliphService.UpdateUserLastLogin(request.Username);
            }
            catch (Exception ex)
            {
                response.StatusCode = APIStatusCode.FAIL_CODE;
                response.StatusMsg = APIStatusCode.FAIL_MSG;

                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
            }
            finally
            {
                functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
                LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddSystemUser([FromBody] SystemUserRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.PW))
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oUsersEnt = oCaliphService.GetSystemUserByUsername(request);
                if (oUsersEnt != null)
                {
                    response.StatusCode = APIStatusCode.USERNAME_EXISTS_CODE;
                    response.StatusMsg = APIStatusCode.USERNAME_EXISTS_MSG;
                    return Ok(response);
                }

                var checkuplinecheckuplineEnt = new SystemUserRequest() { Username = request.UplineUsername };
                var oUplineUsersEnt = oCaliphService.GetSystemUserByUsername(checkuplinecheckuplineEnt);
                if (oUplineUsersEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_UPLINE_USERNAME_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_UPLINE_USERNAME_MSG;
                    return Ok(response);
                }

                var obj = new UsersEnt
                {
                    Username = request.Username,
                    DisplayName = request.DisplayName,
                    Fullname = request.Fullname,
                    PW = request.PW,
                    RoleId = (long)request.RoleId,
                    //UplineUserId = (long)request.UplineUserId,
                    UplineUserId = oUplineUsersEnt.UserId,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    CreatedBy = request.CreatedBy,
                    IcNo = request.IcNo,
                    ContactNo = request.ContactNo,
                    Email = request.Email,
                    JoinDate = request.JoinDate
                };

                var returnId = oCaliphService.AddUsers(obj);

                if (returnId > 0)
                {
                    response.data = returnId;
                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.FAIL_CODE;
                    response.StatusMsg = APIStatusCode.FAIL_MSG;
                }

                response.StatusCode = APIStatusCode.SUCC_CODE;
                response.StatusMsg = APIStatusCode.SUCC_MSG;
            }
            catch (Exception ex)
            {
                response.StatusCode = APIStatusCode.FAIL_CODE;
                response.StatusMsg = APIStatusCode.FAIL_MSG;

                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
            }
            finally
            {
                functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
                LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Master Datas by Master Id
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-filter")]
        public IHttpActionResult GetSystemUserByFilter([FromBody] SystemUserFilter request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oUsersEnt = oCaliphService.GetSystemUserByFilter(request, request.PageSize, request.PageNumber);

                #region Clear pw
                foreach (var item in oUsersEnt)
                {
                    item.PW = "";
                }
                #endregion
                if (oUsersEnt.Count > 0)
                {
                    response.ItemCount = oCaliphService.GetSystemUserByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
                    response.data = oUsersEnt;
                }

                response.data = oUsersEnt;
                response.StatusCode = APIStatusCode.SUCC_CODE;
                response.StatusMsg = APIStatusCode.SUCC_MSG;
            }
            catch (Exception ex)
            {
                response.StatusCode = APIStatusCode.FAIL_CODE;
                response.StatusMsg = APIStatusCode.FAIL_MSG;

                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
            }
            finally
            {
                functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
                LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
            }

            return Ok(response);
        }


        [HttpPost]
        [Route("get-staff-by-filter")]
        public IHttpActionResult GetSystemStaffByFilter([FromBody] SystemUserFilter request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oUsersEnt = oCaliphService.GetSystemStaffByFilter(request, request.PageSize, request.PageNumber);
              
                if (oUsersEnt.Count > 0)
                {
                    response.ItemCount = oCaliphService.GetSystemStaffByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
                    response.data = oUsersEnt;
                }

                response.data = oUsersEnt;
                response.StatusCode = APIStatusCode.SUCC_CODE;
                response.StatusMsg = APIStatusCode.SUCC_MSG;
            }
            catch (Exception ex)
            {
                response.StatusCode = APIStatusCode.FAIL_CODE;
                response.StatusMsg = APIStatusCode.FAIL_MSG;

                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
            }
            finally
            {
                functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
                LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("get-by-id")]
        public IHttpActionResult GetSystemUserById([FromBody] SystemUserRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || !request.UserId.HasValue )
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oUsersEnt = oCaliphService.GetSystemUserByUsername(request);
                if (oUsersEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_SYSTEM_USER_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_SYSTEM_USER_MSG;
                    return Ok(response);
                }

                oUsersEnt.PW = ""; // clear password return fields
                response.data = oUsersEnt;
                response.StatusCode = APIStatusCode.SUCC_CODE;
                response.StatusMsg = APIStatusCode.SUCC_MSG;

                oCaliphService.UpdateUserLastLogin(request.Username);
            }
            catch (Exception ex)
            {
                response.StatusCode = APIStatusCode.FAIL_CODE;
                response.StatusMsg = APIStatusCode.FAIL_MSG;

                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
            }
            finally
            {
                functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
                LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
            }

            return Ok(response);
        }
        [HttpPost]
        [Route("update-pw")]
        public IHttpActionResult UpdateSystemUserPW([FromBody] SystemUserRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.PW))
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var getUsernameRequest = new SystemUserRequest { Username = request.Username };
                var oUsersEnt = oCaliphService.GetSystemUserByUsername(getUsernameRequest);
                if (oUsersEnt == null)
                {
                    response.StatusCode = APIStatusCode.USERNAME_NOT_EXISTS_CODE;
                    response.StatusMsg = APIStatusCode.USERNAME_NOT_EXISTS_MSG;
                    return Ok(response);
                }

                var obj = new UsersEnt
                {
                    Username = request.Username,
                    PW = request.PW,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateUserPW(obj.Username, obj.PW, obj.UpdatedBy);

                if (succ)
                {
                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.FAIL_CODE;
                    response.StatusMsg = APIStatusCode.FAIL_MSG;
                }

                response.StatusCode = APIStatusCode.SUCC_CODE;
                response.StatusMsg = APIStatusCode.SUCC_MSG;
            }
            catch (Exception ex)
            {
                response.StatusCode = APIStatusCode.FAIL_CODE;
                response.StatusMsg = APIStatusCode.FAIL_MSG;

                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
            }
            finally
            {
                functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
                LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("update-status")]
        public IHttpActionResult UpdateSystemUserStatus([FromBody] SystemUserRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || string.IsNullOrEmpty(request.Username) || request.StatusId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var getUsernameRequest = new SystemUserRequest { Username = request.Username };
                var oUsersEnt = oCaliphService.GetSystemUserByUsername(getUsernameRequest);
                if (oUsersEnt == null)
                {
                    response.StatusCode = APIStatusCode.USERNAME_NOT_EXISTS_CODE;
                    response.StatusMsg = APIStatusCode.USERNAME_NOT_EXISTS_MSG;
                    return Ok(response);
                }

                var obj = new UsersEnt
                {
                    Username = request.Username,
                    StatusId = (long)request.StatusId,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateUserStatus(obj.Username, obj.StatusId, obj.UpdatedBy);

                if (succ)
                {
                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.FAIL_CODE;
                    response.StatusMsg = APIStatusCode.FAIL_MSG;
                }

                response.StatusCode = APIStatusCode.SUCC_CODE;
                response.StatusMsg = APIStatusCode.SUCC_MSG;
            }
            catch (Exception ex)
            {
                response.StatusCode = APIStatusCode.FAIL_CODE;
                response.StatusMsg = APIStatusCode.FAIL_MSG;

                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
            }
            finally
            {
                functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
                LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("convert-one2one-agent")]
        public IHttpActionResult ConvertOne2OneAgent([FromBody] ConvertOne2OneAgentRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.NewUsername))
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var getUsernameRequest = new SystemUserRequest { Username = request.Username };
                var oUsersEnt = oCaliphService.GetSystemUserByUsername(getUsernameRequest);
                if (oUsersEnt == null)
                {
                    response.StatusCode = APIStatusCode.USERNAME_NOT_EXISTS_CODE;
                    response.StatusMsg = APIStatusCode.USERNAME_NOT_EXISTS_MSG;
                    return Ok(response);
                }

                var getNewUsernameRequest = new SystemUserRequest { Username = request.NewUsername };
                oUsersEnt = oCaliphService.GetSystemUserByUsername(getNewUsernameRequest);
                if (oUsersEnt != null)
                {
                    response.StatusCode = APIStatusCode.USERNAME_EXISTS_CODE;
                    response.StatusMsg = APIStatusCode.USERNAME_EXISTS_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.ConvertOne2OneAgent(request.Username, request.NewUsername, request.RoleId);

                if (succ)
                {
                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.FAIL_CODE;
                    response.StatusMsg = APIStatusCode.FAIL_MSG;
                }

                response.StatusCode = APIStatusCode.SUCC_CODE;
                response.StatusMsg = APIStatusCode.SUCC_MSG;
            }
            catch (Exception ex)
            {
                response.StatusCode = APIStatusCode.FAIL_CODE;
                response.StatusMsg = APIStatusCode.FAIL_MSG;

                LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
            }
            finally
            {
                functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
                LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
            }

            return Ok(response);
        }
    }
}