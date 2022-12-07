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
    [RoutePrefix("api/v1/agent-simulator")]
    public class AgentSimulatorController : BaseController
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddAgentSimulator([FromBody] AgentSimulatorRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.UserId <= 0)
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

                /* check AgentSimulator year */
                var oFilter = new AgentSimulatorFilter()
                {
                    AgentSimulatorYear = request.AgentSimulatorYear,
                    UserId = request.UserId,
                    PageNumber = ConfigHelper.DefaultPageNo,
                    PageSize = ConfigHelper.DefaultPageSize
                };

                var oList = oCaliphService.GetAgentSimulatorByFilter(oFilter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
                if (oList.Count > 0)
                {
                    response.StatusCode = APIStatusCode.YEAR_EXISTS_CODE;
                    response.StatusMsg = APIStatusCode.YEAR_EXISTS_MSG;
                    return Ok(response);
                }

                var obj = new AgentSimulatorEnt
                {
                    UserId = request.UserId,
                    AgentSimulatorTypeId = request.AgentSimulatorTypeId,
                    AgentSimulatorYear = request.AgentSimulatorYear,
                    AgentSimulatorMonth = request.AgentSimulatorMonth,
                    GrowthPercentage = request.GrowthPercentage,
                    Manpower_YTDRecruit1 = request.Manpower_YTDRecruit1,
                    Manpower_YTDRecruit2 = request.Manpower_YTDRecruit2,
                    Manpower_YTDRecruit3 = request.Manpower_YTDRecruit3,
                    Manpower_YTDRecruit4 = request.Manpower_YTDRecruit4,
                    Manpower_YTDRecruit5 = request.Manpower_YTDRecruit5,
                    Manpower_YTDRecruit6 = request.Manpower_YTDRecruit6,
                    Manpower_YTDRecruit7 = request.Manpower_YTDRecruit7,
                    Manpower_YTDRecruit8 = request.Manpower_YTDRecruit8,
                    Manpower_YTDRecruit9 = request.Manpower_YTDRecruit9,
                    Manpower_YTDRecruit10 = request.Manpower_YTDRecruit10,
                    Manpower_YTDRecruit11 = request.Manpower_YTDRecruit11,
                    Manpower_YTDRecruit12 = request.Manpower_YTDRecruit12,
                    ActiveAgent_YTDRecruit1 = request.ActiveAgent_YTDRecruit1,
                    ActiveAgent_YTDRecruit2 = request.ActiveAgent_YTDRecruit2,
                    ActiveAgent_YTDRecruit3 = request.ActiveAgent_YTDRecruit3,
                    ActiveAgent_YTDRecruit4 = request.ActiveAgent_YTDRecruit4,
                    ActiveAgent_YTDRecruit5 = request.ActiveAgent_YTDRecruit5,
                    ActiveAgent_YTDRecruit6 = request.ActiveAgent_YTDRecruit6,
                    ActiveAgent_YTDRecruit7 = request.ActiveAgent_YTDRecruit7,
                    ActiveAgent_YTDRecruit8 = request.ActiveAgent_YTDRecruit8,
                    ActiveAgent_YTDRecruit9 = request.ActiveAgent_YTDRecruit9,
                    ActiveAgent_YTDRecruit10 = request.ActiveAgent_YTDRecruit10,
                    ActiveAgent_YTDRecruit11 = request.ActiveAgent_YTDRecruit11,
                    ActiveAgent_YTDRecruit12 = request.ActiveAgent_YTDRecruit12,
                    ActiveAgent_TotalCases1 = request.ActiveAgent_TotalCases1,
                    ActiveAgent_TotalCases2 = request.ActiveAgent_TotalCases2,
                    ActiveAgent_TotalCases3 = request.ActiveAgent_TotalCases3,
                    ActiveAgent_TotalCases4 = request.ActiveAgent_TotalCases4,
                    ActiveAgent_TotalCases5 = request.ActiveAgent_TotalCases5,
                    ActiveAgent_TotalCases6 = request.ActiveAgent_TotalCases6,
                    ActiveAgent_TotalCases7 = request.ActiveAgent_TotalCases7,
                    ActiveAgent_TotalCases8 = request.ActiveAgent_TotalCases8,
                    ActiveAgent_TotalCases9 = request.ActiveAgent_TotalCases9,
                    ActiveAgent_TotalCases10 = request.ActiveAgent_TotalCases10,
                    ActiveAgent_TotalCases11 = request.ActiveAgent_TotalCases11,
                    ActiveAgent_TotalCases12 = request.ActiveAgent_TotalCases12,
                    ACE_TotalCases1 = request.ACE_TotalCases1,
                    ACE_TotalCases2 = request.ACE_TotalCases2,
                    ACE_TotalCases3 = request.ACE_TotalCases3,
                    ACE_TotalCases4 = request.ACE_TotalCases4,
                    ACE_TotalCases5 = request.ACE_TotalCases5,
                    ACE_TotalCases6 = request.ACE_TotalCases6,
                    ACE_TotalCases7 = request.ACE_TotalCases7,
                    ACE_TotalCases8 = request.ACE_TotalCases8,
                    ACE_TotalCases9 = request.ACE_TotalCases9,
                    ACE_TotalCases10 = request.ACE_TotalCases10,
                    ACE_TotalCases11 = request.ACE_TotalCases11,
                    ACE_TotalCases12 = request.ACE_TotalCases12,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddAgentSimulator(obj);

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
        public IHttpActionResult UpdateAgentSimulator([FromBody] AgentSimulatorRequest request)
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
                var oAgentSimulatorEnt = oCaliphService.GetAgentSimulatorById(request.AgentSimulatorId);
                if (oAgentSimulatorEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_SIMULATOR_ID;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_SIMULATOR_MSG;
                    return Ok(response);
                }

                var obj = new AgentSimulatorEnt
                {
                    AgentSimulatorId = request.AgentSimulatorId,
                    //UserId = request.UserId,
                    //AgentSimulatorYear = request.AgentSimulatorYear,
                    //AgentSimulatorMonth = request.AgentSimulatorMonth,
                    GrowthPercentage = request.GrowthPercentage,
                    Manpower_YTDRecruit1 = request.Manpower_YTDRecruit1,
                    Manpower_YTDRecruit2 = request.Manpower_YTDRecruit2,
                    Manpower_YTDRecruit3 = request.Manpower_YTDRecruit3,
                    Manpower_YTDRecruit4 = request.Manpower_YTDRecruit4,
                    Manpower_YTDRecruit5 = request.Manpower_YTDRecruit5,
                    Manpower_YTDRecruit6 = request.Manpower_YTDRecruit6,
                    Manpower_YTDRecruit7 = request.Manpower_YTDRecruit7,
                    Manpower_YTDRecruit8 = request.Manpower_YTDRecruit8,
                    Manpower_YTDRecruit9 = request.Manpower_YTDRecruit9,
                    Manpower_YTDRecruit10 = request.Manpower_YTDRecruit10,
                    Manpower_YTDRecruit11 = request.Manpower_YTDRecruit11,
                    Manpower_YTDRecruit12 = request.Manpower_YTDRecruit12,
                    ActiveAgent_YTDRecruit1 = request.ActiveAgent_YTDRecruit1,
                    ActiveAgent_YTDRecruit2 = request.ActiveAgent_YTDRecruit2,
                    ActiveAgent_YTDRecruit3 = request.ActiveAgent_YTDRecruit3,
                    ActiveAgent_YTDRecruit4 = request.ActiveAgent_YTDRecruit4,
                    ActiveAgent_YTDRecruit5 = request.ActiveAgent_YTDRecruit5,
                    ActiveAgent_YTDRecruit6 = request.ActiveAgent_YTDRecruit6,
                    ActiveAgent_YTDRecruit7 = request.ActiveAgent_YTDRecruit7,
                    ActiveAgent_YTDRecruit8 = request.ActiveAgent_YTDRecruit8,
                    ActiveAgent_YTDRecruit9 = request.ActiveAgent_YTDRecruit9,
                    ActiveAgent_YTDRecruit10 = request.ActiveAgent_YTDRecruit10,
                    ActiveAgent_YTDRecruit11 = request.ActiveAgent_YTDRecruit11,
                    ActiveAgent_YTDRecruit12 = request.ActiveAgent_YTDRecruit12,
                    ActiveAgent_TotalCases1 = request.ActiveAgent_TotalCases1,
                    ActiveAgent_TotalCases2 = request.ActiveAgent_TotalCases2,
                    ActiveAgent_TotalCases3 = request.ActiveAgent_TotalCases3,
                    ActiveAgent_TotalCases4 = request.ActiveAgent_TotalCases4,
                    ActiveAgent_TotalCases5 = request.ActiveAgent_TotalCases5,
                    ActiveAgent_TotalCases6 = request.ActiveAgent_TotalCases6,
                    ActiveAgent_TotalCases7 = request.ActiveAgent_TotalCases7,
                    ActiveAgent_TotalCases8 = request.ActiveAgent_TotalCases8,
                    ActiveAgent_TotalCases9 = request.ActiveAgent_TotalCases9,
                    ActiveAgent_TotalCases10 = request.ActiveAgent_TotalCases10,
                    ActiveAgent_TotalCases11 = request.ActiveAgent_TotalCases11,
                    ActiveAgent_TotalCases12 = request.ActiveAgent_TotalCases12,
                    ACE_TotalCases1 = request.ACE_TotalCases1,
                    ACE_TotalCases2 = request.ACE_TotalCases2,
                    ACE_TotalCases3 = request.ACE_TotalCases3,
                    ACE_TotalCases4 = request.ACE_TotalCases4,
                    ACE_TotalCases5 = request.ACE_TotalCases5,
                    ACE_TotalCases6 = request.ACE_TotalCases6,
                    ACE_TotalCases7 = request.ACE_TotalCases7,
                    ACE_TotalCases8 = request.ACE_TotalCases8,
                    ACE_TotalCases9 = request.ACE_TotalCases9,
                    ACE_TotalCases10 = request.ACE_TotalCases10,
                    ACE_TotalCases11 = request.ACE_TotalCases11,
                    ACE_TotalCases12 = request.ACE_TotalCases12,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateAgentSimulator(obj);

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
        public IHttpActionResult GetAgentSimulatorByFilter([FromBody] AgentSimulatorFilter request)
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
                var resultList = oCaliphService.GetAgentSimulatorByFilter(request, request.PageSize, request.PageNumber);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetAgentSimulatorByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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


    }
}