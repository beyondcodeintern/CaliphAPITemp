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
    [RoutePrefix("api/v1/agent-commission")]
    public class AgentCommissionController : BaseController
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddAgentCommission([FromBody] AgentCommissionRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                if (request == null || string.IsNullOrEmpty(request.Username))
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

                #region check dup: agentId + payout date
                var oAgentCommissionFilter = new AgentCommissionFilter()
                {
                    Username = request.Username,
                    PayoutDateFrom = request.PayoutDate,
                    PayoutDateTo = request.PayoutDate
                };

                var oClient = oCaliphService.GetAgentCommissionByFilter(oAgentCommissionFilter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
                if (oClient.Count != 0)
                {
                    response.StatusCode = APIStatusCode.DUPLICATE_CODE;
                    response.StatusMsg = APIStatusCode.DUPLICATE_MSG;
                    return Ok(response);
                }
                #endregion

                var obj = new AgentCommissionEnt
                {
                    Username = request.Username,
                    AgentName = request.AgentName,
                    PayoutDate = request.PayoutDate,
                    CycleStartDate = request.CycleStartDate,
                    CycleEndDate = request.CycleEndDate,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    CreatedBy = request.CreatedBy,
                    CommAmt = request.CommAmt
                };

                var returnId = oCaliphService.AddAgentCommission(obj);

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
        [Route("get-by-filter")]
        public IHttpActionResult GetAgentCommissionByFilter([FromBody] AgentCommissionFilter request)
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
                var oClient = oCaliphService.GetAgentCommissionByFilter(request, request.PageSize, request.PageNumber);
                if (oClient != null)
                {
                    response.ItemCount = oCaliphService.GetAgentCommissionByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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

    }
}