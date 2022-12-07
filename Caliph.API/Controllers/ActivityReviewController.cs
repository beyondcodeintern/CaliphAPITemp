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
    [RoutePrefix("api/v1/activity-review")]
    public class ActivityReviewController : BaseController
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddActivityReview([FromBody] ActivityRequest request)
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

                if (!oCaliphService.IsValidActivityReviewType(MasterEnum.ActivityReviewType, request.ActivityReviewTypeId))
                {
                    response.StatusCode = APIStatusCode.INVALID_ACTIVITY_REVIEW_TYPE_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ACTIVITY_REVIEW_TYPE_ID_MSG;
                    return Ok(response);
                }

                var obj = new ActivityReviewEnt
                {
                    ActivityReviewTypeId = request.ActivityReviewTypeId,
                    UserId = request.UserId,
                    DateInWeek = request.DateInWeek,
                    ReviewText1 = request.ReviewText1,
                    ReviewText2 = request.ReviewText2,
                    ReviewText3 = request.ReviewText3,
                    ReviewText4 = request.ReviewText4,
                    ReviewText5 = request.ReviewText5,
                    ReviewText6 = request.ReviewText6,
                    ReviewText7 = request.ReviewText7,
                    ReviewText8 = request.ReviewText8,
                    ReviewText9 = request.ReviewText9,
                    ReviewText10 = request.ReviewText10,
                    ReviewText11 = request.ReviewText11,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddActivityReview(obj);

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
        public IHttpActionResult UpdateActivityReview([FromBody] ActivityRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.ActivityReviewId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_ACTIVITY_REVIEW_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ACTIVITY_REVIEW_MSG;
                    return Ok(response);
                }

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oActivityReview = oCaliphService.GetActivityReviewById(request.ActivityReviewId);
                if (oActivityReview == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_ACTIVITY_REVIEW_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ACTIVITY_REVIEW_MSG;
                    return Ok(response);
                }

                if (!oCaliphService.IsValidActivityReviewType(MasterEnum.ActivityReviewType, request.ActivityReviewTypeId))
                {
                    response.StatusCode = APIStatusCode.INVALID_ACTIVITY_REVIEW_TYPE_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ACTIVITY_REVIEW_TYPE_ID_MSG;
                    return Ok(response);
                }

                var obj = new ActivityReviewEnt
                {
                    ActivityReviewId = request.ActivityReviewId,
                    ActivityReviewTypeId = request.ActivityReviewTypeId,
                    UserId = request.UserId,
                    DateInWeek = request.DateInWeek,
                    ReviewText1 = request.ReviewText1,
                    ReviewText2 = request.ReviewText2,
                    ReviewText3 = request.ReviewText3,
                    ReviewText4 = request.ReviewText4,
                    ReviewText5 = request.ReviewText5,
                    ReviewText6 = request.ReviewText6,
                    ReviewText7 = request.ReviewText7,
                    ReviewText8 = request.ReviewText8,
                    ReviewText9 = request.ReviewText9,
                    ReviewText10 = request.ReviewText10,
                    ReviewText11 = request.ReviewText11,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateActivityReview(obj);

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
        public IHttpActionResult GetActivityReviewByFilter([FromBody] ActivityReviewFilter request)
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
                var resultList = oCaliphService.GetActivityReviewByFilter(request);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetActivityReviewByFilter(request).Count;
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