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
    [RoutePrefix("api/v1/client-family")]
    public class ClientFamilyController : BaseController
    {
        /// <summary>
        /// Get client Family id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-family-id")]
        public IHttpActionResult GetClientFamilyById([FromBody] ClientFamilyRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.ClientFamilyId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oClient = oCaliphService.GetClientFamilyById(request.ClientFamilyId);
                if (oClient != null)
                {
                    response.data = oClient;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_FAMILY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_FAMILY_MSG;
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
        public IHttpActionResult GetFamilyClientByFilter([FromBody] ClientFamilyFilterRequest request)
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
                var oClient = oCaliphService.GetClientFamilyByFilter(request.ClientId, null, request.StatusId, request.PageSize, request.PageNumber);
                if (oClient != null)
                {
                    response.ItemCount = oCaliphService.GetClientFamilyByFilter(request.ClientId, null, request.StatusId, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
                    response.data = oClient;

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
        [Route("add")]
        public IHttpActionResult AddClientFamily([FromBody] ClientFamilyRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || string.IsNullOrEmpty(request.Name) || request.ClientId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oClient = oCaliphService.GetClientById(request.ClientId,null);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
                    return Ok(response);
                }

                var obj = new ClientFamilyEnt
                {
                    ClientId = request.ClientId,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Name = request.Name,
                    RelationId = request.RelationId,
                    DOB = request.DOB,
                    GenderId = request.GenderId,
                    HobbyDesc = request.HobbyDesc,
                    HPDesc = request.HPDesc,
                    SchoolDesc = request.SchoolDesc,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var succ = oCaliphService.AddClientFamily(obj);

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
        [Route("update")]
        public IHttpActionResult UpdatClientFamily([FromBody] ClientFamilyRequest request)
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

                if (request.ClientFamilyId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_FAMILY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_FAMILY_MSG;
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
                var oClientFamily = oCaliphService.GetClientFamilyById(request.ClientFamilyId);
                if (oClientFamily == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_FAMILY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_FAMILY_MSG;
                    return Ok(response);
                }

                var oUpdateClientFamily = new ClientFamilyEnt
                {
                    ClientFamilyId = request.ClientFamilyId,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Name = request.Name,
                    RelationId = request.RelationId,
                    DOB = request.DOB,
                    GenderId = request.GenderId,
                    HobbyDesc = request.HobbyDesc,
                    HPDesc = request.HPDesc,
                    SchoolDesc = request.SchoolDesc,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateClientFamily(oUpdateClientFamily);
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
        [Route("delete")]
        public IHttpActionResult UpdatClientFamilyStatusInactive([FromBody] ClientFamilyRequest request)
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

                if (request.ClientFamilyId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_FAMILY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_FAMILY_MSG;
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
                var oClientFamily = oCaliphService.GetClientFamilyById(request.ClientFamilyId);
                if (oClientFamily == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_FAMILY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_FAMILY_MSG;
                    return Ok(response);
                }

                if (oClientFamily.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateClientFamilyStatus(request.ClientFamilyId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
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