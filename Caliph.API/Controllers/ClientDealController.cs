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
    [RoutePrefix("api/v1/client-deal")]
    public class ClientDealController : BaseController
    {
        /// <summary>
        /// Get client Deal id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-deal-id")]
        public IHttpActionResult GetClientDealById([FromBody] ClientDealRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.ClientDealId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oClient = oCaliphService.GetClientDealById(request.ClientDealId, request.CreatedBy);
                if (oClient != null)
                {
                    response.data = oClient;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
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
        public IHttpActionResult GetDealClientByFilter([FromBody] ClientDealFilterRequest request)
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
                var oClient = oCaliphService.GetClientDealByFilter(request.ClientId, null, request.StatusId, request.DealTitleId, request.Name, request.ClientName, 
                    request.CreatedBy, request.ClientCreatedBy, request.CreatedDateFrom,request.CreatedDateTo, request.PageSize, request.PageNumber);
                if (oClient != null)
                {
                    response.ItemCount = oCaliphService.GetClientDealByFilter(request.ClientId, null, request.StatusId, request.DealTitleId, request.Name, request.ClientName, 
                        request.CreatedBy, request.ClientCreatedBy, request.CreatedDateFrom, request.CreatedDateTo, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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
        public IHttpActionResult AddClientDeal([FromBody] ClientDealRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.ClientId <= 0)
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
                var oClient = oCaliphService.GetClientById(request.ClientId, null);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
                    return Ok(response);
                }

                var obj = new ClientDealEnt
                {
                    ClientId = request.ClientId,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    DealTitleId = request.DealTitleId,
                    Name = request.Name,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var succ = oCaliphService.AddClientDeal(obj);

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
        public IHttpActionResult UpdatClientDeal([FromBody] ClientDealRequest request)
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

                if (request.ClientDealId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
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
                var oClientDeal = oCaliphService.GetClientDealById(request.ClientDealId);
                if (oClientDeal == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
                    return Ok(response);
                }

                var oUpdateClientDeal = new ClientDealEnt
                {
                    ClientDealId = request.ClientDealId,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    DealTitleId = request.DealTitleId,
                    Name = request.Name,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateClientDeal(oUpdateClientDeal);
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
        public IHttpActionResult UpdatClientDealStatusInactive([FromBody] ClientDealRequest request)
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

                if (request.ClientDealId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
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
                var oClientDeal = oCaliphService.GetClientDealById(request.ClientDealId);
                if (oClientDeal == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
                    return Ok(response);
                }

                if (oClientDeal.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateClientDealStatus(request.ClientDealId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy, oClientDeal.ClientId);
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
        [Route("update-active")]
        public IHttpActionResult UpdatClientDealStatusConfirm([FromBody] ClientDealRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Active);
        }

        [HttpPost]
        [Route("update-closed")]
        public IHttpActionResult UpdatClientDealStatusPotential([FromBody] ClientDealRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Closed);
        }

        [HttpPost]
        [Route("update-lost")]
        public IHttpActionResult UpdatClientDealStatusKIV([FromBody] ClientDealRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Lost);
        }

        private IHttpActionResult UpdateStatus(ClientDealRequest request, long updateStatus)
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

                if (request.ClientDealId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
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
                var oClientDeal = oCaliphService.GetClientDealById(request.ClientDealId);
                if (oClientDeal == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_DEAL_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_DEAL_MSG;
                    return Ok(response);
                }

                if (oClientDeal.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateClientDealStatus(request.ClientDealId, updateStatus, request.UpdatedBy, oClientDeal.ClientId);
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