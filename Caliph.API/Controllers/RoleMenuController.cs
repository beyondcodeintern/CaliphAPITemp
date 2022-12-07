using Caliph.API.Models;
using Caliph.Library;
using Caliph.Library.Helper;
using Caliph.Library.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Caliph.API.Controllers
{
    [RoutePrefix("api/v1/role-menu")]
    public class RoleMenuController : BaseController
    {
        [HttpPost]
        [Route("get-all-menu")]
        public IHttpActionResult GetAllMenu()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oMenuList = oCaliphService.GetAllMenu();

                response.data = oMenuList;
                response.ItemCount =((oMenuList==null|| oMenuList.Menus == null)? 0: oMenuList.Menus.Count);
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
        [Route("add")]
        public IHttpActionResult AddRoleMenu([FromBody] RoleRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_SYSTEM_USER_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_SYSTEM_USER_MSG;
                    return Ok(response);
                }

               

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();

                var obj = new RolesEnt
                {
                    Code = request.Code,
                    Name = request.Name,
                    SubMenuIds = request.SubMenuIdList,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddRole(obj);

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
        [Route("update")]
        public IHttpActionResult UpdateRole([FromBody] RoleRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_SYSTEM_USER_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_SYSTEM_USER_MSG;
                    return Ok(response);
                }

                if (request.RoleId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_ROLE_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ROLE_ID_MSG;
                    return Ok(response);
                }

               

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                if (oCaliphService.GetRoleById(request.RoleId) == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_ROLE_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ROLE_ID_MSG;
                    return Ok(response);
                }

                var obj = new RolesEnt
                {
                    RoleId = request.RoleId,
                    Code = request.Code,
                    Name = request.Name,
                    SubMenuIds = request.SubMenuIdList,
                    UpdatedBy= request.UpdatedBy
                };

                var succ = oCaliphService.UpdateRole(obj);

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
        [Route("get-by-filter")]
        public IHttpActionResult GetRoleByFilter([FromBody] RoleFilter request)
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
                var resultList = oCaliphService.GetRoleByFilter(request, request.PageSize, request.PageNumber);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetRoleByFilter(request, request.PageSize, request.PageNumber).Count;
                    response.data = resultList;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
                }
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
        public IHttpActionResult GetRoleById([FromBody] RoleFilter request)
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


                if (request.RoleId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_ROLE_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ROLE_ID_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oClient = oCaliphService.GetRoleById(request.RoleId);





                if (oClient == null)
                {

                    response.StatusCode = APIStatusCode.FAIL_CODE;
                    response.StatusMsg = APIStatusCode.FAIL_MSG;
                }
                else if (oClient.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.INVALID_ROLE_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ROLE_ID_MSG;
                    return Ok(response);
                }
                else
                {
                    response.data = oClient;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }

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
        [Route("delete")]
        public IHttpActionResult DeleteRole([FromBody] RoleRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                #region Validation
                if (request == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                if (request.RoleId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_ROLE_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ROLE_ID_MSG;
                    return Ok(response);
                }

                if (String.IsNullOrEmpty(request.UpdatedBy))
                {
                    response.StatusCode = APIStatusCode.INVALID_UPDATED_BY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_UPDATED_BY_MSG;
                    return Ok(response);
                }
                #endregion

                var oCaliphService = new CaliphService();
                var oClient = oCaliphService.GetRoleById(request.RoleId);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_ROLE_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ROLE_ID_MSG;
                    return Ok(response);
                }

                if (oClient.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateRoleStatus(request.RoleId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
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