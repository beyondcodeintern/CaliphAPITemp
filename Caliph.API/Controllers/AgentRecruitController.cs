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
    [RoutePrefix("api/v1/agent-recruit")]
    public class AgentRecruitController : BaseController
    {
        #region agent recruit
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddAgentRecruit([FromBody] AgentRecruitRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();

                var obj = new AgentRecruitEnt
                {
                    Name = request.Name,
                    ContactNo = request.ContactNo,
                    EducationBgId = request.EducationBgId,
                    AgeId = request.AgeId,
                    StatusId = (long)MasterDataEnum.Status_Potential,
                    EmailAdd = request.EmailAdd,
                    AnnualIncomeId = request.AnnualIncomeId,
                    OccupationId = request.OccupationId,
                    MaritalId = request.MaritalId,
                    TypeId = request.TypeId,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddAgentRecruit(obj);

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
        public IHttpActionResult UpdateAgentRecruit([FromBody] AgentRecruitRequest request)
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
                var oAgentRecruitEnt = oCaliphService.GetAgentRecruitById(request.AgentRecruitId);
                if (oAgentRecruitEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_RECRUIT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_RECRUIT_MSG;
                    return Ok(response);
                }

                var obj = new AgentRecruitEnt
                {
                    AgentRecruitId = request.AgentRecruitId,
                    Name = request.Name,
                    ContactNo = request.ContactNo,
                    EducationBgId = request.EducationBgId,
                    AgeId = request.AgeId,
                    EmailAdd = request.EmailAdd,
                    AnnualIncomeId = request.AnnualIncomeId,
                    OccupationId = request.OccupationId,
                    MaritalId = request.MaritalId,
                    TypeId = request.TypeId,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateAgentRecruit(obj);

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
        [Route("update-status")]
        public IHttpActionResult UpdateAgentRecruitStatus([FromBody] AgentRecruitRequest request)
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
                var oAgentRecruitEnt = oCaliphService.GetAgentRecruitById(request.AgentRecruitId);
                if (oAgentRecruitEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_RECRUIT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_RECRUIT_MSG;
                    return Ok(response);
                }

                var obj = new AgentRecruitEnt
                {
                    AgentRecruitId = request.AgentRecruitId,
                    StatusId = request.StatusId,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateAgentRecruitStatus(obj);

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
        public IHttpActionResult GetAgentRecruitByFilter([FromBody] AgentRecruitFilter request)
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
                var resultList = oCaliphService.GetAgentRecruitByFilter(request, request.PageSize, request.PageNumber);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetAgentRecruitByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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
        #endregion

        #region agent recruit track
        [HttpPost]
        [Route("track-add")]
        public IHttpActionResult AddAgentRecruitTrack([FromBody] AgentRecruitTrackRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oAgentRecruitEnt = oCaliphService.GetAgentRecruitById(request.AgentRecruitId);
                if (oAgentRecruitEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_RECRUIT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_RECRUIT_MSG;
                    return Ok(response);
                }

                var obj = new AgentRecruitTrackEnt
                {
                    AgentRecruitId = request.AgentRecruitId,
                    TrackRemarks = request.TrackRemarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddAgentRecruitTrack(obj);

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
        [Route("track-update")]
        public IHttpActionResult UpdateAgentRecruitTrack([FromBody] AgentRecruitTrackRequest request)
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
                var oAgentRecruitTrackEnt = oCaliphService.GetAgentRecruitTrackById(request.AgentRecruitTrackId);
                if (oAgentRecruitTrackEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_AGENT_RECRUIT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_AGENT_RECRUIT_MSG;
                    return Ok(response);
                }

                var obj = new AgentRecruitTrackEnt
                {
                    AgentRecruitTrackId = request.AgentRecruitTrackId,
                    TrackRemarks = request.TrackRemarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateAgentRecruitTrack(obj);

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
        [Route("track-get-by-filter")]
        public IHttpActionResult GetAgentRecruitTrackByFilter([FromBody] AgentRecruitTrackFilter request)
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
                var resultList = oCaliphService.GetAgentRecruitTrackByFilter(request, request.PageSize, request.PageNumber);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetAgentRecruitTrackByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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
        #endregion
    }
}