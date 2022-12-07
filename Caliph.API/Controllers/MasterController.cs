using Caliph.API.Models;
using Caliph.Library;
using Caliph.Library.Helper;
using Caliph.Library.Models;
using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Linq;

namespace Caliph.API.Controllers
{
    [RoutePrefix("api/v1/master")]
    public class MasterController : BaseController
    {
        /// <summary>
        /// Get Master Datas by Master Id
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-by-masterid")]
        public IHttpActionResult GetMasterDataByMasterId([FromBody] MasterRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.MasterId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();
                response.data = oCaliphService.GetMasterDataByMasterId(request.MasterId);

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
        /// Get Master Datas by Master Id
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-client-status")]
        public IHttpActionResult GetClientStatus()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oStatusList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.SystemStatus);
                oStatusList = oStatusList.Where(x => x.MasterDataId == (long)MasterDataEnum.Status_Potential ||
                x.MasterDataId == (long)MasterDataEnum.Status_Confirm ||
                x.MasterDataId == (long)MasterDataEnum.Status_Inactive ||
                x.MasterDataId == (long)MasterDataEnum.Status_Archive).ToList();

                response.data = oStatusList;
                response.ItemCount = oStatusList.Count;
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
        [Route("get-deal-status")]
        public IHttpActionResult GetDealStatus()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oStatusList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.SystemStatus);
                oStatusList = oStatusList.Where(x => x.MasterDataId == (long)MasterDataEnum.Status_Closed ||
                x.MasterDataId == (long)MasterDataEnum.Status_Active ||
                x.MasterDataId == (long)MasterDataEnum.Status_Lost ||
                x.MasterDataId == (long)MasterDataEnum.Status_Inactive).ToList();

                response.data = oStatusList;
                response.ItemCount = oStatusList.Count;
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
        [Route("get-client-source")]
        public IHttpActionResult GetClientSource()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.Source);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-annual-income")]
        public IHttpActionResult GetAnnualIncome()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.AnnualIncome);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-occupation")]
        public IHttpActionResult GetOccupation()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.Occupation);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-marital-status")]
        public IHttpActionResult GetMaritalStatus()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.MaritalStatus);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-length-of-time-known")]
        public IHttpActionResult GetLengthOfTimeKnown()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.LengthOfTimeKnown);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-how-well-known")]
        public IHttpActionResult GetHowWellKnown()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.HowWellKnown);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-how-often-seen-in-a-year")]
        public IHttpActionResult GetHowOftenSeenInAYear()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.HowOftenSeenInAYear);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-could-approach-on-business")]
        public IHttpActionResult GetCouldApproachOnBusiness()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.CouldApproachOnBusiness);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-ability-to-provide-referrals")]
        public IHttpActionResult GetAbilityToProvideReferrals()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.AbilityToProvideReferrals);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-age-range")]
        public IHttpActionResult GetAgeRange()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.AgeRange);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-gender")]
        public IHttpActionResult GetGender()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.Gender);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-deal-title")]
        public IHttpActionResult GetDealTitle()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.DealTitle);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-client-relationship")]
        public IHttpActionResult GetClientRelationship()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.ClientRelationship);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-activity-status")]
        public IHttpActionResult GetActivityStatus()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oStatusList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.SystemStatus);
                oStatusList = oStatusList.Where(x => x.MasterDataId == (long)MasterDataEnum.Status_Missed ||
                x.MasterDataId == (long)MasterDataEnum.Status_Active ||
                x.MasterDataId == (long)MasterDataEnum.Status_Done ||
                x.MasterDataId == (long)MasterDataEnum.Status_Inactive).ToList();

                response.data = oStatusList;
                response.ItemCount = oStatusList.Count;
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
        [Route("get-lead-status")]
        public IHttpActionResult GetLeadStatus()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oStatusList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.SystemStatus);
                oStatusList = oStatusList.Where(x => x.MasterDataId == (long)MasterDataEnum.Status_Leads ||
                x.MasterDataId == (long)MasterDataEnum.Status_ConvertedToClient ||
                x.MasterDataId == (long)MasterDataEnum.Status_Inactive).ToList();

                response.data = oStatusList;
                response.ItemCount = oStatusList.Count;
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
        [Route("get-activity")]
        public IHttpActionResult GetActivity()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetActivityPoint();
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-announcement-type")]
        public IHttpActionResult GetAnnouncementType()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.AnnouncementType);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-activity-review-type")]
        public IHttpActionResult GetActivityReviewType()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.ActivityReviewType);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-monthly-budget-type")]
        public IHttpActionResult GetMonthlyBudgetType()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.MonthlyBudgetType);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-education-bg")]
        public IHttpActionResult GetEducationBg()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.EducationBg);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-agent-simulator-type")]
        public IHttpActionResult GetAgentSimulatorType()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.AgentSimulatorType);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-event-type")]
        public IHttpActionResult GetEventType()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.EventType);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-event-host")]
        public IHttpActionResult GetEventHost()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.EventHost);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-event-channel")]
        public IHttpActionResult GetEventChannel()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.EventChannel);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-attendant-type")]
        public IHttpActionResult GetAttendantType()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.AttendantType);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-quiz-score")]
        public IHttpActionResult GetQuizScore()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.QuizScore);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-user-event-status")]
        public IHttpActionResult GetUserEventStatus()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.UserEventStatusId);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-payment-channel")]
        public IHttpActionResult GetPaymentChannel()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.PaymentChannel);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-payment-status")]
        public IHttpActionResult GetPaymentStatus()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.PaymentStatus);
                response.data = oList;
                response.ItemCount = oList.Count;
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
        [Route("get-resource-status")]
        public IHttpActionResult GetResourceStatus()
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                var oCaliphService = new CaliphService();
                var oStatusList = oCaliphService.GetMasterDataByMasterId((long)MasterEnum.SystemStatus);
                oStatusList = oStatusList.Where(x => x.MasterDataId == (long)MasterDataEnum.Status_Potential ||
                x.MasterDataId == (long)MasterDataEnum.Status_Confirm ||
                x.MasterDataId == (long)MasterDataEnum.Status_Inactive ||
                x.MasterDataId == (long)MasterDataEnum.Status_Archive).ToList();

                response.data = oStatusList;
                response.ItemCount = oStatusList.Count;
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
    }
}