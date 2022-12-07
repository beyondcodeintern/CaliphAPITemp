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
    [RoutePrefix("api/v1/client")]
    public class ClientController : BaseController
    {
        /// <summary>
        /// Add Client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddClient([FromBody] ClientRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || string.IsNullOrEmpty(request.Name))
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

                var duplicateClient = oCaliphService.GetDuplicateClientHpByAgent(request.ContactNo, request.CreatedBy);
                if (duplicateClient != null)
                {
                    response.StatusCode = APIStatusCode.DUPLICATE_CLIENT_CONTACT;
                    response.StatusMsg = APIStatusCode.DUPLICATE_CLIENT_CONTACT_MSG;
                    return Ok(response);
                }

                var oClient = new ClientsEnt
                {
                    Name = request.Name,
                    NickName = request.NickName,
                    ICNo = request.ICNo,
                    ContactNo = request.ContactNo,
                    EmailAdd = request.EmailAdd,
                    SourceId = request.SourceId,
                    StatusId = (long)MasterDataEnum.Status_Potential,
                    AnnualIncomeId = request.AnnualIncomeId,
                    AgeId = request.AgeId,
                    OccupationId = request.OccupationId,
                    MaritalId = request.MaritalId,
                    LengthOfTimeKnownId = request.LengthOfTimeKnownId,
                    HowWellKnownId = request.HowWellKnownId,
                    HowOftenSeenInAYearId = request.HowOftenSeenInAYearId,
                    CouldApproachOnBusinessId = request.CouldApproachOnBusinessId,
                    AbilityToProvideRefId = request.AbilityToProvideRefId,
                    GenderId = request.GenderId,
                    EducationDesc = request.EducationDesc,
                    IncomeYearDesc = request.IncomeYearDesc,
                    OtherSourceofIncomeDesc = request.OtherSourceofIncomeDesc,
                    CareerDesc = request.CareerDesc,
                    FilingDate = request.FilingDate,
                    CreatedBy = request.CreatedBy
                };

                var succ = oCaliphService.AddClient(oClient);

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

        /// <summary>
        /// Get all client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-all")]
        public IHttpActionResult GetAllClient([FromBody] object request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                List<ClientDetailsEnt> oClientList = oCaliphService.GetClientAll();
                if (oClientList.Count > 0)
                {
                    response.data = oClientList;
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
        /// Get client
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-client-id")]
        public IHttpActionResult GetClientById([FromBody] ClientRequest request)
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

                var oCaliphService = new CaliphService();
                var oClient = oCaliphService.GetClientById(request.ClientId, request.CreatedBy);
                if (oClient != null)
                {
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

        /// <summary>
        /// Get all client by filter
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-client-filter")]
        public IHttpActionResult GetClientByFilter([FromBody] ClientFilterRequest request)
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
                var oClient = oCaliphService.GetClientByFilter(request.ClientId, request.StatusId, request.KIVDateFrom, request.KIVDateTo, request.CreatedDateFrom, request.CreatedDateTo, request.PageSize, request.PageNumber, true, request.CreatedBy, request.StatusIdList, request.Name);
                if (oClient != null)
                {
                    response.ItemCount = oCaliphService.GetClientByFilter(request.ClientId, request.StatusId, request.KIVDateFrom, request.KIVDateTo, request.CreatedDateFrom, request.CreatedDateTo, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo, true, request.CreatedBy, request.StatusIdList, request.Name).Count;
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

        /// <summary>
        /// Get client KIV list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-kiv")]
        public IHttpActionResult GetClientKIVList([FromBody] ClientFilterRequest request)
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
                var oClient = oCaliphService.GetClientByFilter(null, (long)MasterDataEnum.Status_KIV, request.KIVDateFrom, request.KIVDateTo, request.CreatedDateFrom, request.CreatedDateTo, request.PageSize, request.PageNumber, false, request.CreatedBy, request.Name);
                if (oClient != null)
                {
                    response.ItemCount = oCaliphService.GetClientByFilter(null, (long)MasterDataEnum.Status_KIV, request.KIVDateFrom, request.KIVDateTo, request.CreatedDateFrom, request.CreatedDateTo, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo, false, request.CreatedBy, request.Name).Count;
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

        /// <summary>
        /// Get client KIV list
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("revert-kiv")]
        public IHttpActionResult UpdatClientStatusKIV([FromBody] ClientRequest request)
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

                if (request.ClientId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
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
                var oClient = oCaliphService.GetClientById(request.ClientId, null);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
                    return Ok(response);
                }

                if (oClient.StatusId != (long)MasterDataEnum.Status_KIV)
                {
                    response.StatusCode = APIStatusCode.ALREADY_REVERT_STATUS_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_REVERT_STATUS_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateClientStatus(request.ClientId, (long)MasterDataEnum.Status_Potential, request.UpdatedBy, true);
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
        [Route("update-basic-info")]
        public IHttpActionResult UpdatClientBasicInfo([FromBody] ClientRequest request)
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

                if (request.ClientId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
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
                var oClient = oCaliphService.GetClientById(request.ClientId, null);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
                    return Ok(response);
                }

                var oUpdateClient = new ClientsEnt
                {
                    ClientId = request.ClientId,
                    Name = request.Name,
                    NickName = request.NickName,
                    ICNo = request.ICNo,
                    ContactNo = request.ContactNo,
                    EmailAdd = request.EmailAdd,
                    DOB = request.DOB,
                    SourceId = request.SourceId,
                    AnnualIncomeId = request.AnnualIncomeId,
                    AgeId = request.AgeId,
                    OccupationId = request.OccupationId,
                    MaritalId = request.MaritalId,
                    OtherSourceofIncomeDesc = request.OtherSourceofIncomeDesc,
                    GenderId = request.GenderId,
                    CurrentAddress = request.CurrentAddress,
                    EducationDesc = request.EducationDesc,
                    CareerDesc = request.CareerDesc,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateClientBasicInfo(oUpdateClient);
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
        [Route("update-relationship-info")]
        public IHttpActionResult UpdatClientRelationshipInfo([FromBody] ClientRequest request)
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

                if (request.ClientId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
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
                var oClient = oCaliphService.GetClientById(request.ClientId, null);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
                    return Ok(response);
                }

                var oUpdateClient = new ClientsEnt
                {
                    ClientId = request.ClientId,
                    FilingDate = request.FilingDate,
                    LengthOfTimeKnownId = request.LengthOfTimeKnownId,
                    HowWellKnownId = request.HowWellKnownId,
                    HowOftenSeenInAYearId = request.HowOftenSeenInAYearId,
                    CouldApproachOnBusinessId = request.CouldApproachOnBusinessId,
                    AbilityToProvideRefId = request.AbilityToProvideRefId,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateClientRelationshipInfo(oUpdateClient);
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
        public IHttpActionResult UpdatClientStatusInactive([FromBody] ClientRequest request)
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

                if (request.ClientId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
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
                var oClient = oCaliphService.GetClientById(request.ClientId, null);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
                    return Ok(response);
                }

                if (oClient.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateClientStatus(request.ClientId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
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
        [Route("update-potential")]
        public IHttpActionResult UpdatClientStatusPotential([FromBody] ClientRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Potential);
        }

        [HttpPost]
        [Route("update-confirm")]
        public IHttpActionResult UpdatClientStatusConfirm([FromBody] ClientRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Confirm);
        }

        [HttpPost]
        [Route("update-archive")]
        public IHttpActionResult UpdatClientStatusarchive([FromBody] ClientRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Archive);
        }

        private IHttpActionResult UpdateStatus(ClientRequest request, long updateStatus)
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

                if (request.ClientId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
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
                var oClient = oCaliphService.GetClientById(request.ClientId, null);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_CLIENT_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_CLIENT_MSG;
                    return Ok(response);
                }

                if (oClient.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateClientStatus(request.ClientId, updateStatus, request.UpdatedBy);
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