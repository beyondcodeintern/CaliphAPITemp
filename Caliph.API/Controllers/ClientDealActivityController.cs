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
    [RoutePrefix("api/v1/client-deal-activity")]
    public class ClientDealActivityController : BaseController
    {
        /// <summary>
        /// Get client activity id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-deal-activity-id")]
        public IHttpActionResult GetClientActivityById([FromBody] ClientActivityRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.ClientDealActivityId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oClient = oCaliphService.GetClientDealActivityById(request.ClientDealActivityId, request.CreatedBy);
                if (oClient != null)
                {
                    response.data = oClient;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_MSG;
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
        public IHttpActionResult GetActivityClientByFilter([FromBody] ClientActivityFilterRequest request)
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
                var oClient = oCaliphService.GetClientDealActivityByFilter(request.ClientId, request.ClientDealActivityId, request.DealTitleId, request.ClientDealId, request.StatusId, request.ActivityStartDate, request.ActivityEndDate, request.CreatedBy, request.ClientDealStatusId, request.PageSize, request.PageNumber);
                if (oClient != null)
                {
                    response.ItemCount = oCaliphService.GetClientDealActivityByFilter(request.ClientId, request.ClientDealActivityId, request.DealTitleId, request.ClientDealId, request.StatusId, request.ActivityStartDate, request.ActivityEndDate, request.CreatedBy, request.ClientDealStatusId, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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
        public IHttpActionResult AddClientActivity([FromBody] ClientActivityRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.ClientDealId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
                    return Ok(response);
                }

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oClientDeal = oCaliphService.GetClientDealById(request.ClientDealId);
                if (oClientDeal == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
                    return Ok(response);
                }

                var obj = new ClientActivityEnt
                {
                    ClientDealId = request.ClientDealId,
                    ActivityPointId = request.ActivityPointId,
                    Points = 0,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    ActivityStartDate = request.ActivityStartDate,
                    ActivityEndDate = request.ActivityEndDate,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddClientDealActivity(obj);

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
        public IHttpActionResult UpdatClientActivity([FromBody] ClientActivityRequest request)
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

                if (request.ClientDealActivityId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_MSG;
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
                var oClientActivity = oCaliphService.GetClientDealActivityById(request.ClientDealActivityId);
                if (oClientActivity == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_MSG;
                    return Ok(response);
                }

                var oUpdateClientActivity = new ClientActivityEnt
                {
                    ClientDealActivityId = request.ClientDealActivityId,
                    ActivityPointId = request.ActivityPointId,
                    ActivityStartDate = request.ActivityStartDate,
                    ActivityEndDate = request.ActivityEndDate,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateClientDealActivity(oUpdateClientActivity);
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
        public IHttpActionResult UpdatClientActivityStatusInactive([FromBody] ClientActivityRequest request)
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

                if (request.ClientDealActivityId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_MSG;
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
                var oClientActivity = oCaliphService.GetClientDealActivityById(request.ClientDealActivityId);
                if (oClientActivity == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_MSG;
                    return Ok(response);
                }

                if (oClientActivity.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                #region Get points
                int points = 0;
                #endregion

                var succ = oCaliphService.UpdateClientDealActivityStatus(request.ClientDealActivityId, (long)MasterDataEnum.Status_Inactive, points, request.UpdatedBy);
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
        [Route("update-missed")]
        public IHttpActionResult UpdatClientActivityStatusMissed([FromBody] ClientActivityRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Missed);
        }

        [HttpPost]
        [Route("update-done")]
        public IHttpActionResult UpdatClientActivityStatusDone([FromBody] ClientActivityRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Done);
        }

        [HttpPost]
        [Route("update-active")]
        public IHttpActionResult UpdatClientActivityStatusActive([FromBody] ClientActivityRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Active);
        }

        private IHttpActionResult UpdateStatus(ClientActivityRequest request, long updateStatus)
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

                if (request.ClientDealActivityId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_MSG;
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
                var oClientActivity = oCaliphService.GetClientDealActivityById(request.ClientDealActivityId);
                if (oClientActivity == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_ACTIVITY_MSG;
                    return Ok(response);
                }

                if (oClientActivity.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                #region Get points
                int points = 0;
                if (updateStatus == (long)MasterDataEnum.Status_Done)
                {
                    points = oClientActivity.PointSetting;
                }
                #endregion

                var succ = oCaliphService.UpdateClientDealActivityStatus(request.ClientDealActivityId, updateStatus, points, request.UpdatedBy);
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