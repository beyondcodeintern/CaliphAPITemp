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
    [RoutePrefix("api/v1/client-lead")]
    public class ClientLeadController : BaseController
    {
        /// <summary>
        /// Get client lead id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-lead-id")]
        public IHttpActionResult GetClientLeadById([FromBody] ClientLeadRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.ClientLeadId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oClient = oCaliphService.GetClientLeadById(request.ClientLeadId);
                if (oClient != null)
                {
                    response.data = oClient;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_LEAD_MSG;
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
        public IHttpActionResult GetLeadClientByFilter([FromBody] ClientLeadFilterRequest request)
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
                var oClient = oCaliphService.GetClientLeadByFilter(request.ClientLeadId, request.clientDealActivityId, request.StatusId, request.PageSize, request.PageNumber);
                if (oClient != null)
                {
                    response.ItemCount = oCaliphService.GetClientLeadByFilter(request.ClientLeadId, request.clientDealActivityId, request.StatusId, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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
        public IHttpActionResult AddClientLead([FromBody] ClientLeadRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.ClientDealActivityId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_LEAD_MSG;
                    return Ok(response);
                }

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oClientActivity = oCaliphService.GetClientDealActivityById(request.ClientDealActivityId);
                if (oClientActivity == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_MSG;
                    return Ok(response);
                }

                var obj = new ClientLeadEnt
                {
                    ClientDealActivityId = request.ClientDealActivityId,
                    ClientId = request.ClientId,
                    StatusId = (long)MasterDataEnum.Status_Leads,
                    Name = request.Name,
                    HP = request.HP,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var succ = oCaliphService.AddClientLead(obj);

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
        public IHttpActionResult UpdatClientLead([FromBody] ClientLeadRequest request)
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

                if (request.ClientLeadId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_LEAD_MSG;
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
                var oClientLead = oCaliphService.GetClientLeadById(request.ClientLeadId);
                if (oClientLead == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_LEAD_MSG;
                    return Ok(response);
                }

                var oUpdateClientLead = new ClientLeadEnt
                {
                    ClientLeadId = request.ClientLeadId,
                    ClientId = request.ClientId,
                    Name = request.Name,
                    HP = request.HP,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateClientLead(oUpdateClientLead);
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
        public IHttpActionResult UpdatClientLeadStatusInactive([FromBody] ClientLeadRequest request)
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

                if (request.ClientLeadId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_LEAD_MSG;
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
                var oClientLead = oCaliphService.GetClientLeadById(request.ClientLeadId);
                if (oClientLead == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_LEAD_MSG;
                    return Ok(response);
                }

                if (oClientLead.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateClientLeadStatus(request.ClientLeadId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
                oCaliphService.CheckRefLeadEntitlement(oClientLead.ClientDealActivityId);
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
        [Route("update-to-lead-status")]
        public IHttpActionResult UpdatClientLeadStatusLead([FromBody] ClientLeadRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Leads);
        }

        [HttpPost]
        [Route("update-convert-to-client-status")]
        public IHttpActionResult UpdatClientLeadStatusConvertToClient([FromBody] ClientLeadRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_ConvertedToClient);
        }

        private IHttpActionResult UpdateStatus([FromBody] ClientLeadRequest request, long updateStatus)
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

                if (request.ClientLeadId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_LEAD_MSG;
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
                var oClientLead = oCaliphService.GetClientLeadById(request.ClientLeadId);
                if (oClientLead == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_LEAD_MSG;
                    return Ok(response);
                }

                if (oClientLead.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateClientLeadStatus(request.ClientLeadId, updateStatus, request.UpdatedBy);
                oCaliphService.CheckRefLeadEntitlement(oClientLead.ClientDealActivityId);
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