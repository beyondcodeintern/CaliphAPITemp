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
    [RoutePrefix("api/v1/agent-lead")]
    public class AgentLeadController : BaseController
    {
        /// <summary>
        /// Get agent lead id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-lead-id")]
        public IHttpActionResult GetAgentLeadById([FromBody] AgentLeadRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.AgentLeadId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                var oAgent = oCaliphService.GetAgentLeadById(request.AgentLeadId);
                if (oAgent != null)
                {
                    response.data = oAgent;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_LEAD_MSG;
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
        public IHttpActionResult GetLeadAgentByFilter([FromBody] AgentLeadFilter request)
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
                var oAgent = oCaliphService.GetAgentLeadByFilter(request, request.PageSize, request.PageNumber);
                if (oAgent != null)
                {
                    response.ItemCount = oCaliphService.GetAgentLeadByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
                    response.data = oAgent;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_ACTIVITY_MSG;
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
        public IHttpActionResult AddAgentLead([FromBody] AgentLeadRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.AgentActivityId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_ACTIVITY_MSG;
                    return Ok(response);
                }

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oAgentActivity = oCaliphService.GetAgentActivityById(request.AgentActivityId);
                if (oAgentActivity == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_ACTIVITY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_ACTIVITY_MSG;
                    return Ok(response);
                }

                var obj = new AgentLeadEnt
                {
                    AgentActivityId = request.AgentActivityId,
                    AgentId = request.AgentId,
                    StatusId = (long)MasterDataEnum.Status_Leads,
                    Name = request.Name,
                    HP = request.HP,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddAgentLead(obj);

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
        public IHttpActionResult UpdatAgentLead([FromBody] AgentLeadRequest request)
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

                if (request.AgentLeadId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_LEAD_MSG;
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
                var oAgentLead = oCaliphService.GetAgentLeadById(request.AgentLeadId);
                if (oAgentLead == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_LEAD_MSG;
                    return Ok(response);
                }

                var oUpdateAgentLead = new AgentLeadEnt
                {
                    AgentLeadId = request.AgentLeadId,
                    AgentId = request.AgentId,
                    Name = request.Name,
                    HP = request.HP,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateAgentLead(oUpdateAgentLead);
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
        public IHttpActionResult UpdatAgentLeadStatusInactive([FromBody] AgentLeadRequest request)
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

                if (request.AgentLeadId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_LEAD_MSG;
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
                var oAgentLead = oCaliphService.GetAgentLeadById(request.AgentLeadId);
                if (oAgentLead == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_LEAD_MSG;
                    return Ok(response);
                }

                if (oAgentLead.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateAgentLeadStatus(request.AgentLeadId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
                // oCaliphService.CheckRefLeadEntitlement(oAgentLead.AgentActivityId);
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
        public IHttpActionResult UpdatAgentLeadStatusLead([FromBody] AgentLeadRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Leads);
        }

        [HttpPost]
        [Route("update-convert-to-agent-status")]
        public IHttpActionResult UpdatAgentLeadStatusConvertToAgent([FromBody] AgentLeadRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_ConvertedToAgent);
        }

        private IHttpActionResult UpdateStatus([FromBody] AgentLeadRequest request, long updateStatus)
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

                if (request.AgentLeadId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_LEAD_MSG;
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
                var oAgentLead = oCaliphService.GetAgentLeadById(request.AgentLeadId);
                if (oAgentLead == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_LEAD_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_LEAD_MSG;
                    return Ok(response);
                }

                if (oAgentLead.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateAgentLeadStatus(request.AgentLeadId, updateStatus, request.UpdatedBy);
                oCaliphService.CheckAgentRefLeadEntitlement(oAgentLead.AgentActivityId);
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