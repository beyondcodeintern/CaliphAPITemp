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
    [RoutePrefix("api/v1/announcement")]
    public class AnnouncementController : BaseController
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddAnnouncement([FromBody] AnnouncementRequest request)
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

                if (request.AnnouncementTypeId != (long)AnnouncementType.all && request.AnnouncementTypeId != (long)AnnouncementType.specified_user)
                {
                    response.StatusCode = APIStatusCode.ANNOUNCEMENT_TYPE_CODE;
                    response.StatusMsg = APIStatusCode.ANNOUNCEMENT_TYPE_MSG;
                    return Ok(response);
                }

                if (request.AnnouncementTypeId == (long)AnnouncementType.specified_user && (request.UserIdList == null || request.UserIdList.Count == 0))
                {
                    response.StatusCode = APIStatusCode.USER_LIST_REQ_CODE;
                    response.StatusMsg = APIStatusCode.USER_LIST_REQ_MSG;
                    return Ok(response);
                }

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();

                var obj = new AnnouncementEnt
                {
                    Title = request.Title,
                    AnnouncementTypeId = request.AnnouncementTypeId,
                    PublishStartDate = request.PublishStartDate,
                    PublishEndDate = request.PublishEndDate,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddAnnouncement(obj, request.UserIdList);

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
        public IHttpActionResult UpdateAnnouncement([FromBody] AnnouncementRequest request)
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

                if (request.AnnouncementId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_ANNOUNCEMENT_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ANNOUNCEMENT_ID_MSG;
                    return Ok(response);
                }

                if (request.AnnouncementTypeId != (long)AnnouncementType.all && request.AnnouncementTypeId != (long)AnnouncementType.specified_user)
                {
                    response.StatusCode = APIStatusCode.ANNOUNCEMENT_TYPE_CODE;
                    response.StatusMsg = APIStatusCode.ANNOUNCEMENT_TYPE_MSG;
                    return Ok(response);
                }

                if (request.AnnouncementTypeId == (long)AnnouncementType.specified_user && (request.UserIdList == null || request.UserIdList.Count == 0))
                {
                    response.StatusCode = APIStatusCode.USER_LIST_REQ_CODE;
                    response.StatusMsg = APIStatusCode.USER_LIST_REQ_MSG;
                    return Ok(response);
                }

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                if (oCaliphService.GetAnnouncementById(request.AnnouncementId) == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_ANNOUNCEMENT_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ANNOUNCEMENT_ID_MSG;
                    return Ok(response);
                }

                var obj = new AnnouncementEnt
                {
                    AnnouncementId = request.AnnouncementId,
                    Title = request.Title,
                    AnnouncementTypeId = request.AnnouncementTypeId,
                    PublishStartDate = request.PublishStartDate,
                    PublishEndDate = request.PublishEndDate,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateAnnouncement(obj, request.UserIdList);

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
        public IHttpActionResult GetAnnouncementByFilter([FromBody] AnnouncementFilter request)
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
                var resultList = oCaliphService.GetAnnouncementByFilter(request, request.PageSize, request.PageNumber);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetAnnouncementByFilter(request, request.PageSize, request.PageNumber).Count;
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



        [HttpPost]
        [Route("delete")]
        public IHttpActionResult DeleteAnnoouncement([FromBody] AnnouncementRequest request)
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

                if (request.AnnouncementId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_ANNOUNCEMENT_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ANNOUNCEMENT_ID_MSG;
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
                var oClient = oCaliphService.GetAnnouncementById(request.AnnouncementId);
                if (oClient == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_ANNOUNCEMENT_ID_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_ANNOUNCEMENT_ID_MSG;
                    return Ok(response);
                }

                if (oClient.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateAnnouncementStatus(request.AnnouncementId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
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