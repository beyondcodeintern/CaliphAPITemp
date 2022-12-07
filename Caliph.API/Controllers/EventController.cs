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
    [RoutePrefix("api/v1/event")]
    public class EventController : BaseController
    {
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddEvent([FromBody] EventRequest request)
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

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();

                var obj = new EventEnt
                {
                    EventName = request.EventName,
                    EventTypeId = request.EventTypeId,
                    EventHostId = request.EventHostId,
                    EventChannelId = request.EventChannelId,
                    EventChannelLocation = request.EventChannelLocation,
                    EventFees = request.EventFees,
                    Remarks = request.Remarks,
                    PaxLimit = request.PaxLimit,
                    CPDPoint = request.CPDPoint,
                    AttendantTypeId = request.AttendantTypeId,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    CreatedBy = request.CreatedBy,
                };

                if (request.EventRoleIds != null)
                {
                    foreach (var role in request.EventRoleIds)
                    {
                        obj.EventRoleList.Add(new EventRoleEnt { EventRoleId= role });
                    }
                }

                var returnId = oCaliphService.AddEvent(obj);

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
        public IHttpActionResult UpdatEvent([FromBody] EventRequest request)
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

                if (request.EventId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_MSG;
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
                var oEvent = oCaliphService.GetEventById(request.EventId);
                if (oEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_MSG;
                    return Ok(response);
                }

                var oUpdateEvent = new EventEnt
                {
                    EventId = request.EventId,
                    EventName = request.EventName,
                    EventTypeId = request.EventTypeId,
                    EventHostId = request.EventHostId,
                    EventChannelId = request.EventChannelId,
                    EventChannelLocation = request.EventChannelLocation,
                    EventFees = request.EventFees,
                    Remarks = request.Remarks,
                    PaxLimit = request.PaxLimit,
                    CPDPoint = request.CPDPoint,
                    AttendantTypeId = request.AttendantTypeId,
                    UpdatedBy = request.UpdatedBy,
                    
                };
                if (request.EventRoleIds != null)
                {
                    foreach (var role in request.EventRoleIds)
                    {
                        oUpdateEvent.EventRoleList.Add(new EventRoleEnt { EventRoleId = role });
                    }
                }

                var succ = oCaliphService.UpdateEvent(oUpdateEvent);
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
        public IHttpActionResult GetEventByFilter([FromBody] EventFilter request)
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
                var oAgent = oCaliphService.GetEventByFilter(request, request.PageSize, request.PageNumber);
                if (oAgent != null)
                {
                    response.ItemCount = oCaliphService.GetEventByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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
        [Route("upcoming-get-by-filter")]
        public IHttpActionResult GetUpcomingEventByFilter([FromBody] EventFilter request)
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
                var oAgent = oCaliphService.GetUpcomingEventByFilter(request, request.PageSize, request.PageNumber);
                if (oAgent != null)
                {
                    response.ItemCount = oCaliphService.GetUpcomingEventByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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
        [Route("delete")]
        public IHttpActionResult UpdatEventStatusInactive([FromBody] EventRequest request)
        {
            return UpdateStatus(request, (long)MasterDataEnum.Status_Inactive);
        }

        //[HttpPost]
        //[Route("update-active")]
        //public IHttpActionResult UpdatEventStatusActive([FromBody] EventRequest request)
        //{
        //    return UpdateStatus(request, (long)MasterDataEnum.Status_Active);
        //}

        private IHttpActionResult UpdateStatus(EventRequest request, long updateStatus)
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

                if (request.EventId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_MSG;
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
                var oEvent = oCaliphService.GetEventById(request.EventId);
                if (oEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_MSG;
                    return Ok(response);
                }

                if (oEvent.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateEventStatus(request.EventId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
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

        #region Event Date
        [HttpPost]
        [Route("date-add")]
        public IHttpActionResult AddEventDate([FromBody] EventDateRequest request)
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

                if (request.EventId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_MSG;
                    return Ok(response);
                }
                #endregion

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oEvent = oCaliphService.GetEventById(request.EventId);

                if (oEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_MSG;
                    return Ok(response);
                }

                if (request.EventDateFrom > request.EventDateTo)
                {
                    response.StatusCode = APIStatusCode.INVALID_DATE_RANGE_ID;
                    response.StatusMsg = APIStatusCode.INVALID_DATE_RANGE_MSG;
                    return Ok(response);
                }

                var obj = new EventDateEnt
                {
                    EventId = request.EventId,
                    EventDateFrom = request.EventDateFrom,
                    EventDateTo = request.EventDateTo,
                    RegClosingDate = request.RegClosingDate,
                    EventDateStatusId = (long)MasterDataEnum.Status_Active,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddEventDate(obj);

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
        [Route("date-update")]
        public IHttpActionResult UpdatEventDate([FromBody] EventDateRequest request)
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

                if (request.EventDateId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_DATE_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_DATE_MSG;
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
                var oEventDate = oCaliphService.GetEventDateById(request.EventDateId);
                if (oEventDate == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_DATE_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_DATE_MSG;
                    return Ok(response);
                }

                var oUpdateEventDate = new EventDateEnt
                {
                    EventDateId = request.EventDateId,
                    EventDateFrom = request.EventDateFrom,
                    EventDateTo = request.EventDateTo,
                    RegClosingDate = request.RegClosingDate,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateEventDate(oUpdateEventDate);
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
        [Route("date-delete")]
        public IHttpActionResult UpdatEventDateStatusInactive([FromBody] EventDateRequest request)
        {
            return UpdateEventDateStatus(request, (long)MasterDataEnum.Status_Inactive);
        }

        private IHttpActionResult UpdateEventDateStatus(EventDateRequest request, long updateStatus)
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

                if (request.EventDateId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_DATE_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_DATE_MSG;
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
                var oEventDate = oCaliphService.GetEventDateById(request.EventDateId);
                if (oEventDate == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_DATE_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_DATE_MSG;
                    return Ok(response);
                }

                if (oEventDate.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateEventDateStatus(request.EventDateId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
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
        [Route("date-get-by-filter")]
        public IHttpActionResult GetEventDateByFilter([FromBody] EventDateFilter request)
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
                var oList = oCaliphService.GetEventDateByFilter(request, "", request.PageSize, request.PageNumber);
                if (oList != null)
                {
                    response.ItemCount = oCaliphService.GetEventDateByFilter(request, "", ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
                    response.data = oList;

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

        #endregion

        #region Event Attachment
        [HttpPost]
        [Route("attachment-add")]
        public IHttpActionResult AddEventAttachment([FromBody] EventAttachmentRequest request)
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

                if (request.EventId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_MSG;
                    return Ok(response);
                }
                #endregion

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oEvent = oCaliphService.GetEventById(request.EventId);

                if (oEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_MSG;
                    return Ok(response);
                }

                var obj = new EventAttachmentEnt
                {
                    EventAttachmentId = request.EventAttachmentId,
                    EventId = request.EventId,
                    EventAttachmentName = request.EventAttachmentName,
                    EventAttachmentPath = request.EventAttachmentPath,
                    Remarks = request.Remarks,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddEventAttachment(obj);

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
        [Route("attachment-update")]
        public IHttpActionResult UpdatEventAttachment([FromBody] EventAttachmentRequest request)
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

                if (request.EventAttachmentId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ATTACHMENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_ATTACHMENT_MSG;
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
                var oEventAttachment = oCaliphService.GetEventAttachmentById(request.EventAttachmentId);
                if (oEventAttachment == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ATTACHMENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_ATTACHMENT_MSG;
                    return Ok(response);
                }

                var oUpdateEventAttachment = new EventAttachmentEnt
                {
                    EventAttachmentId = request.EventAttachmentId,
                    EventAttachmentName = request.EventAttachmentName,
                    EventAttachmentPath = request.EventAttachmentPath,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateEventAttachment(oUpdateEventAttachment);
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
        [Route("attachment-delete")]
        public IHttpActionResult UpdatEventAttachmentStatusInactive([FromBody] EventAttachmentRequest request)
        {
            return UpdateEventAttachmentStatus(request, (long)MasterDataEnum.Status_Inactive);
        }

        private IHttpActionResult UpdateEventAttachmentStatus(EventAttachmentRequest request, long updateStatus)
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

                if (request.EventAttachmentId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ATTACHMENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_ATTACHMENT_MSG;
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
                var oEventAttachment = oCaliphService.GetEventAttachmentById(request.EventAttachmentId);
                if (oEventAttachment == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ATTACHMENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_ATTACHMENT_MSG;
                    return Ok(response);
                }

                if (oEventAttachment.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateEventAttachmentStatus(request.EventAttachmentId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy);
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
        [Route("attachment-get-by-filter")]
        public IHttpActionResult GetEventAttachmentByFilter([FromBody] EventAttachmentFilter request)
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
                var oList = oCaliphService.GetEventAttachmentByFilter(request, request.PageSize, request.PageNumber);
                if (oList != null)
                {
                    response.ItemCount = oCaliphService.GetEventAttachmentByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
                    response.data = oList;

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

        #endregion

        #region User Event
        [HttpPost]
        [Route("user-add")]
        public IHttpActionResult AddUserEvent([FromBody] UserEventRequest request)
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

                if (request.EventDateId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_DATE_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_DATE_MSG;
                    return Ok(response);
                }
                #endregion

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oEvent = oCaliphService.GetEventDateById(request.EventDateId);

                if (oEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_DATE_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_DATE_MSG;
                    return Ok(response);
                }

                var obj = new UserEventEnt
                {
                    UserId = request.UserId,
                    EventDateId = request.EventDateId,
                    AttendanceId = request.AttendanceId,
                    QuizScoreId = request.QuizScoreId,
                    CPDPoint = request.CPDPoint,
                    IsEmailSent = request.IsEmailSent,
                    Remarks = request.Remarks,
                    StatusId = request.StatusId>0?request.StatusId: (long)MasterDataEnum.UserEventStatusId_Pending,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddUserEvent(obj);

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
        [Route("user-update")]
        public IHttpActionResult UpdatUserEvent([FromBody] UserEventRequest request)
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

                if (request.UserEventId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_USER_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_USER_EVENT_MSG;
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
                var oUserEvent = oCaliphService.GetUserEventById(request.UserEventId);
                if (oUserEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_USER_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_USER_EVENT_MSG;
                    return Ok(response);
                }

                var oUpdateUserEvent = new UserEventEnt
                {
                    UserEventId = request.UserEventId,
                    //UserId = request.UserId,
                    EventDateId = request.EventDateId,
                    AttendanceId = request.AttendanceId,
                    QuizScoreId = request.QuizScoreId,
                    CPDPoint = request.CPDPoint,
                    IsEmailSent = request.IsEmailSent,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateUserEvent(oUpdateUserEvent);
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
        [Route("user-update-attendance")]
        public IHttpActionResult UpdateUserEventAttendance([FromBody] UserEventRequest request)
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

                if (request.UserEventId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_USER_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_USER_EVENT_MSG;
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
                var oUserEvent = oCaliphService.GetUserEventById(request.UserEventId);
                if (oUserEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_USER_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_USER_EVENT_MSG;
                    return Ok(response);
                }

                if (oUserEvent.AttendanceId == request.AttendanceId)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateUserEventAttendance(request.UserEventId, request.AttendanceId, request.UpdatedBy);
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
        [Route("user-update-status")]
        public IHttpActionResult UpdateUserEventStatus([FromBody] UserEventRequest request)
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

                if (request.UserEventId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_USER_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_USER_EVENT_MSG;
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
                var oUserEvent = oCaliphService.GetUserEventById(request.UserEventId);
                if (oUserEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_USER_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_USER_EVENT_MSG;
                    return Ok(response);
                }

                if (oUserEvent.AttendanceId == request.AttendanceId)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateUserEventStatus(request.UserEventId, request.StatusId, request.UpdatedBy);
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
        [Route("user-get-by-filter")]
        public IHttpActionResult GetUserEventByFilter([FromBody] UserEventFilter request)
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
                var oList = oCaliphService.GetUserEventByFilter(request, request.PageSize, request.PageNumber);
                if (oList != null)
                {
                    response.ItemCount = oCaliphService.GetUserEventByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
                    response.data = oList;

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

        #endregion

        #region User Event Payment
        [HttpPost]
        [Route("user-payment-add")]
        public IHttpActionResult AddUserEventPayment([FromBody] UserEventPaymentRequest request)
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

                if (request.UserEventId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_USER_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_USER_EVENT_MSG;
                    return Ok(response);
                }
                #endregion

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oEvent = oCaliphService.GetUserEventById(request.UserEventId);

                if (oEvent == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_USER_EVENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_USER_EVENT_MSG;
                    return Ok(response);
                }

                string userEventPaymentRefNo = $"{request.PaymentChannelId}-{request.UserEventId}-{Guid.NewGuid().ToString().Substring(0, 20).Replace("-","")}";
                var obj = new UserEventPaymentEnt
                {
                    UserEventId = request.UserEventId,
                    UserEventPaymentRefNo = userEventPaymentRefNo,
                    PaymentChannelId = request.PaymentChannelId,
                    PaymentRefId = request.PaymentRefId,
                    PaymentResponse = request.PaymentResponse,
                    PayementStatusId = request.PayementStatusId,
                    PayementCreatedDate = DateTime.Now,
                    Remarks = request.Remarks,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    CreatedBy = request.CreatedBy
                };

                var oUserEventPaymentResponse = new UserEventPaymentResponse();
                var returnId = oCaliphService.AddUserEventPayment(obj);

                oUserEventPaymentResponse.UserEventPaymentId = returnId;
                oUserEventPaymentResponse.UserEventPaymentRefNo = userEventPaymentRefNo;

                if (returnId > 0)
                {
                    response.data = oUserEventPaymentResponse;
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
        [Route("user-payment-update")]
        public IHttpActionResult UpdatUserEventPayment([FromBody] UserEventPaymentRequest request)
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

                if (request.UserEventPaymentId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ATTACHMENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_ATTACHMENT_MSG;
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
                var oUserEventPayment = oCaliphService.GetUserEventPaymentById(request.UserEventPaymentId);
                if (oUserEventPayment == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_EVENT_ATTACHMENT_ID;
                    response.StatusMsg = APIStatusCode.INVALID_EVENT_ATTACHMENT_MSG;
                    return Ok(response);
                }

                var oUpdateUserEventPayment = new UserEventPaymentEnt
                {
                    UserEventPaymentId = request.UserEventPaymentId,
                    PaymentRefId = request.PaymentRefId,
                    PaymentResponse = request.PaymentResponse,
                    PayementStatusId = request.PayementStatusId,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateUserEventPayment(oUpdateUserEventPayment);
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
        [Route("user-payment-get-by-filter")]
        public IHttpActionResult GetUserEventPaymentByFilter([FromBody] UserEventPaymentFilter request)
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
                var oList = oCaliphService.GetUserEventPaymentByFilter(request, request.PageSize, request.PageNumber);
                if (oList != null)
                {
                    response.ItemCount = oCaliphService.GetUserEventPaymentByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
                    response.data = oList;

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

        #endregion
    }
}