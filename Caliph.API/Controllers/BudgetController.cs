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
    [RoutePrefix("api/v1/budget")]
    public class BudgetController : BaseController
    {
        [HttpPost]
        [Route("simulator-add")]
        public IHttpActionResult AddBudget([FromBody] BudgetRequest request)
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

                /* check budget year */
                var oBudgetFilter = new BudgetFilter()
                {
                    BudgetYear = request.BudgetYear,
                    UserId = request.UserId,
                    PageNumber = ConfigHelper.DefaultPageNo,
                    PageSize = ConfigHelper.DefaultPageSize
                };

                var oList = oCaliphService.GetBudgetByFilter(oBudgetFilter, false);
                if (oList.Count > 0)
                {
                    response.StatusCode = APIStatusCode.BUDGET_YEAR_EXISTS_CODE;
                    response.StatusMsg = APIStatusCode.BUDGET_YEAR_EXISTS_MSG;
                    return Ok(response);
                }

                var obj = new BudgetEnt
                {
                    UserId = request.UserId,
                    BudgetYear = request.BudgetYear,
                    BudgetMonth = request.BudgetMonth,
                    ProductPricePlan = request.ProductPricePlan,
                    ProductStartMonth = request.ProductStartMonth,
                    ProductQtyMonth1 = request.ProductQtyMonth1,
                    ProductQtyMonth2 = request.ProductQtyMonth2,
                    ProductQtyMonth3 = request.ProductQtyMonth3,
                    ProductQtyMonth4 = request.ProductQtyMonth4,
                    ProductQtyMonth5 = request.ProductQtyMonth5,
                    ProductQtyMonth6 = request.ProductQtyMonth6,
                    ProductQtyMonth7 = request.ProductQtyMonth7,
                    ProductQtyMonth8 = request.ProductQtyMonth8,
                    ProductQtyMonth9 = request.ProductQtyMonth9,
                    ProductQtyMonth10 = request.ProductQtyMonth10,
                    ProductQtyMonth11 = request.ProductQtyMonth11,
                    ProductQtyMonth12 = request.ProductQtyMonth12,
                    RecruitmentCount1 = request.RecruitmentCount1,
                    RecruitmentCount2 = request.RecruitmentCount2,
                    RecruitmentCount3 = request.RecruitmentCount3,
                    RecruitmentCount4 = request.RecruitmentCount4,
                    RecruitmentCount5 = request.RecruitmentCount5,
                    RecruitmentCount6 = request.RecruitmentCount6,
                    RecruitmentCount7 = request.RecruitmentCount7,
                    RecruitmentCount8 = request.RecruitmentCount8,
                    RecruitmentCount9 = request.RecruitmentCount9,
                    RecruitmentCount10 = request.RecruitmentCount10,
                    RecruitmentCount11 = request.RecruitmentCount11,
                    RecruitmentCount12 = request.RecruitmentCount12,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddBudget(obj);

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
        [Route("simulator-update")]
        public IHttpActionResult UpdateBudget([FromBody] BudgetRequest request)
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
                var oBudgetEnt = oCaliphService.GetBudgetById(request.BudgetId);
                if (oBudgetEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_BUGDET_ID;
                    response.StatusMsg = APIStatusCode.INVALID_BUGDET_MSG;
                    return Ok(response);
                }

                var obj = new BudgetEnt
                {
                    BudgetId = request.BudgetId,
                    UserId = request.UserId,
                    //BudgetYear = request.BudgetYear,
                    BudgetMonth = request.BudgetMonth,
                    ProductPricePlan = request.ProductPricePlan,
                    ProductStartMonth = request.ProductStartMonth,
                    ProductQtyMonth1 = request.ProductQtyMonth1,
                    ProductQtyMonth2 = request.ProductQtyMonth2,
                    ProductQtyMonth3 = request.ProductQtyMonth3,
                    ProductQtyMonth4 = request.ProductQtyMonth4,
                    ProductQtyMonth5 = request.ProductQtyMonth5,
                    ProductQtyMonth6 = request.ProductQtyMonth6,
                    ProductQtyMonth7 = request.ProductQtyMonth7,
                    ProductQtyMonth8 = request.ProductQtyMonth8,
                    ProductQtyMonth9 = request.ProductQtyMonth9,
                    ProductQtyMonth10 = request.ProductQtyMonth10,
                    ProductQtyMonth11 = request.ProductQtyMonth11,
                    ProductQtyMonth12 = request.ProductQtyMonth12,
                    RecruitmentCount1 = request.RecruitmentCount1,
                    RecruitmentCount2 = request.RecruitmentCount2,
                    RecruitmentCount3 = request.RecruitmentCount3,
                    RecruitmentCount4 = request.RecruitmentCount4,
                    RecruitmentCount5 = request.RecruitmentCount5,
                    RecruitmentCount6 = request.RecruitmentCount6,
                    RecruitmentCount7 = request.RecruitmentCount7,
                    RecruitmentCount8 = request.RecruitmentCount8,
                    RecruitmentCount9 = request.RecruitmentCount9,
                    RecruitmentCount10 = request.RecruitmentCount10,
                    RecruitmentCount11 = request.RecruitmentCount11,
                    RecruitmentCount12 = request.RecruitmentCount12,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateBudget(obj);

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
        [Route("simulator-get-by-filter")]
        public IHttpActionResult GetBudgetByFilter([FromBody] BudgetFilter request)
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
                var resultList = oCaliphService.GetBudgetByFilter(request, true);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetBudgetByFilter(request, false).Count;
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
        [Route("proportion-update")]
        public IHttpActionResult UpdateBudgetProportion([FromBody] BudgetPropotionRequest request)
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
                var oBudgetEnt = oCaliphService.GetBudgetById(request.BudgetId);
                if (oBudgetEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_BUGDET_ID;
                    response.StatusMsg = APIStatusCode.INVALID_BUGDET_MSG;
                    return Ok(response);
                }

                var obj = new BudgetProportionEnt
                {
                    BudgetId = request.BudgetId,
                    BudgetProportionStartMonth = request.BudgetProportionStartMonth,
                    BudgetProportionPercentage1 = request.BudgetProportionPercentage1,
                    BudgetProportionPercentage2 = request.BudgetProportionPercentage2,
                    BudgetProportionPercentage3 = request.BudgetProportionPercentage3,
                    BudgetProportionPercentage4 = request.BudgetProportionPercentage4,
                    BudgetProportionPercentage5 = request.BudgetProportionPercentage5,
                    BudgetProportionPercentage6 = request.BudgetProportionPercentage6,
                    BudgetProportionPercentage7 = request.BudgetProportionPercentage7,
                    BudgetProportionPercentage8 = request.BudgetProportionPercentage8,
                    BudgetProportionPercentage9 = request.BudgetProportionPercentage9,
                    BudgetProportionPercentage10 = request.BudgetProportionPercentage10,
                    BudgetProportionPercentage11 = request.BudgetProportionPercentage11,
                    BudgetProportionPercentage12 = request.BudgetProportionPercentage12,
                    BudgetProportionAmt1 = request.BudgetProportionAmt1,
                    BudgetProportionAmt2 = request.BudgetProportionAmt2,
                    BudgetProportionAmt3 = request.BudgetProportionAmt3,
                    BudgetProportionAmt4 = request.BudgetProportionAmt4,
                    BudgetProportionAmt5 = request.BudgetProportionAmt5,
                    BudgetProportionAmt6 = request.BudgetProportionAmt6,
                    BudgetProportionAmt7 = request.BudgetProportionAmt7,
                    BudgetProportionAmt8 = request.BudgetProportionAmt8,
                    BudgetProportionAmt9 = request.BudgetProportionAmt9,
                    BudgetProportionAmt10 = request.BudgetProportionAmt10,
                    BudgetProportionAmt11 = request.BudgetProportionAmt11,
                    BudgetProportionAmt12 = request.BudgetProportionAmt12,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateBudgetProportion(obj);

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

        #region budget group
        [HttpPost]
        [Route("group-add")]
        public IHttpActionResult AddBudgetGroup([FromBody] BudgetGroupRequest request)
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

                /* check budget year */
                var oBudgetFilter = new BudgetFilter()
                {
                    BudgetId = request.BudgetId,
                    PageNumber = ConfigHelper.DefaultPageNo,
                    PageSize = ConfigHelper.DefaultPageSize
                };

                var oList = oCaliphService.GetBudgetByFilter(oBudgetFilter, false);
                if (oList.Count <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_BUGDET_ID;
                    response.StatusMsg = APIStatusCode.INVALID_BUGDET_MSG;
                    return Ok(response);
                }

                var obj = new BudgetGroupEnt
                {
                    UserId = request.UserId,
                    BudgetId = request.BudgetId,
                    BudgetTitle = request.BudgetTitle,
                    TargetCount = request.TargetCount,
                    TargetComm = request.TargetComm,
                    TotalCase = request.TotalCase,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddBudgetGroup(obj);

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
        [Route("group-update")]
        public IHttpActionResult UpdateBudgetGroup([FromBody] BudgetGroupRequest request)
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
                var oBudgetEnt = oCaliphService.GetBudgetGroupById(request.BudgetGroupId);
                if (oBudgetEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_BUGDET_GROUP_ID;
                    response.StatusMsg = APIStatusCode.INVALID_BUGDET_GROUP_MSG;
                    return Ok(response);
                }

                var obj = new BudgetGroupEnt
                {
                    BudgetGroupId = request.BudgetGroupId,
                    UserId = request.UserId,
                    BudgetTitle = request.BudgetTitle,
                    TargetCount = request.TargetCount,
                    TargetComm = request.TargetComm,
                    TotalCase = request.TotalCase,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateBudgetGroup(obj);

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
        [Route("group-delete")]
        public IHttpActionResult UpdateBudgetGroupStatus([FromBody] BudgetGroupRequest request)
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
                var oBudgetEnt = oCaliphService.GetBudgetGroupById(request.BudgetGroupId);
                if (oBudgetEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_BUGDET_GROUP_ID;
                    response.StatusMsg = APIStatusCode.INVALID_BUGDET_GROUP_MSG;
                    return Ok(response);
                }

                long updateStatus = (long)MasterDataEnum.Status_Inactive;
                if (oBudgetEnt.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateStatusBudgetGroup(request.BudgetGroupId, updateStatus, request.UpdatedBy);

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
        #endregion

        #region monthly budget
        [HttpPost]
        [Route("monthly-add")]
        public IHttpActionResult AddMonthlyBudget([FromBody] BudgetMonthlyRequest request)
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

                /* check */
                //var oBudgetMonthlyFilter = new BudgetMonthlyFilter()
                //{
                //    UserId = request.UserId,
                //    BudgetYear = request.BudgetYear,
                //    BudgetMonth = request.BudgetMonth,
                //    MonthlyBudgetTypeId = request.MonthlyBudgetTypeId,
                //    PageNumber = ConfigHelper.DefaultPageNo,
                //    PageSize = ConfigHelper.DefaultPageSize
                //};

                //var oList = oCaliphService.GetBudgetMonthlyByFilter(oBudgetMonthlyFilter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
                //if (oList.Count > 0)
                //{
                //    response.StatusCode = APIStatusCode.RECORD_EXISTS_CODE;
                //    response.StatusMsg = APIStatusCode.RECORD_EXISTS_MSG;
                //    return Ok(response);
                //}

                var obj = new BudgetMonthlyEnt
                {
                    UserId = request.UserId,
                    BudgetYear = request.BudgetYear,
                    BudgetMonth = request.BudgetMonth,
                    MonthlyBudgetTypeId = request.MonthlyBudgetTypeId,
                    MonthlyBudgetPercentage = request.MonthlyBudgetPercentage,
                    NoOfCases = request.NoOfCases,
                    ClientId = request.ClientId,
                    PersonName = request.PersonName,
                    BudgetValue = request.BudgetValue,
                    AchieveValue = request.AchieveValue,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddBudgetMonthly(obj);

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
        [Route("monthly-update")]
        public IHttpActionResult UpdateMonthlyBudget([FromBody] BudgetMonthlyRequest request)
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
                var oBudgetEnt = oCaliphService.GetBudgetMonthlyById(request.BudgetMonthlyId);
                if (oBudgetEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_MONTHLY_BUGDET_ID;
                    response.StatusMsg = APIStatusCode.INVALID_MONTHLY_BUGDET_MSG;
                    return Ok(response);
                }

                var obj = new BudgetMonthlyEnt
                {
                    BudgetMonthlyId = request.BudgetMonthlyId,
                    MonthlyBudgetPercentage = request.MonthlyBudgetPercentage,
                    NoOfCases = request.NoOfCases,
                    ClientId = request.ClientId,
                    PersonName = request.PersonName,
                    BudgetValue = request.BudgetValue,
                    AchieveValue = request.AchieveValue,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateBudgetMonthly(obj);

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
        [Route("monthly-delete")]
        public IHttpActionResult UpdateMonthlyBudgetStatus([FromBody] BudgetMonthlyRequest request)
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
                var oBudgetEnt = oCaliphService.GetBudgetMonthlyById(request.BudgetMonthlyId);
                if (oBudgetEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_MONTHLY_BUGDET_ID;
                    response.StatusMsg = APIStatusCode.INVALID_MONTHLY_BUGDET_MSG;
                    return Ok(response);
                }

                long updateStatus = (long)MasterDataEnum.Status_Inactive;
                if (oBudgetEnt.StatusId == updateStatus)
                {
                    response.StatusCode = APIStatusCode.ALREADY_UPDATED_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_UPDATED_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateStatusBudgetMonthly(request.BudgetMonthlyId, updateStatus, request.UpdatedBy);

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
        [Route("monthly-get-by-filter")]
        public IHttpActionResult GetMonthlyBudgetByFilter([FromBody] BudgetMonthlyFilter request)
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
                var resultList = oCaliphService.GetBudgetMonthlyByFilter(request, request.PageSize, request.PageNumber);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetBudgetMonthlyByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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

        #region budget strategy
        [HttpPost]
        [Route("strategy-add")]
        public IHttpActionResult AddBudgetStrategy([FromBody] BudgetStrategyRequest request)
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

                /* check */
                var oBudgetStrategyFilter = new BudgetStrategyFilter()
                {
                    UserId = request.UserId,
                    BudgetStrategyYear = request.BudgetStrategyYear,
                    PageNumber = ConfigHelper.DefaultPageNo,
                    PageSize = ConfigHelper.DefaultPageSize
                };

                var oList = oCaliphService.GetBudgetStrategyByFilter(oBudgetStrategyFilter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
                if (oList.Count > 0)
                {
                    response.StatusCode = APIStatusCode.RECORD_EXISTS_CODE;
                    response.StatusMsg = APIStatusCode.RECORD_EXISTS_MSG;
                    return Ok(response);
                }

                var obj = new BudgetStrategyEnt
                {
                    UserId = request.UserId,
                    BudgetStrategyYear = request.BudgetStrategyYear,
                    NoOfCasesForTheYear = request.NoOfCasesForTheYear,
                    GoalACEValue = request.GoalACEValue,
                    HighEndPercentage = request.HighEndPercentage,
                    LowEndPercentage = request.LowEndPercentage,
                    HighEndACEValue = request.HighEndACEValue,
                    LowEndACEValue = request.LowEndACEValue,
                    HighEndAveragePremium = request.HighEndAveragePremium,
                    LowEndAveragePremium = request.LowEndAveragePremium,
                    HighEndNoOfCases = request.HighEndNoOfCases,
                    LowEndNoOfCases = request.LowEndNoOfCases,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy
                };

                var returnId = oCaliphService.AddBudgetStrategy(obj);

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
        [Route("strategy-update")]
        public IHttpActionResult UpdateBudgetStrategy([FromBody] BudgetStrategyRequest request)
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
                var oBudgetStrategyEnt = oCaliphService.GetBudgetStrategyById(request.BudgetStrategyId);
                if (oBudgetStrategyEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_BUGDET_STRATEGY_ID;
                    response.StatusMsg = APIStatusCode.INVALID_BUGDET_STRATEGY_MSG;
                    return Ok(response);
                }

                var obj = new BudgetStrategyEnt
                {
                    BudgetStrategyId = request.BudgetStrategyId,
                    NoOfCasesForTheYear = request.NoOfCasesForTheYear,
                    GoalACEValue = request.GoalACEValue,
                    HighEndPercentage = request.HighEndPercentage,
                    LowEndPercentage = request.LowEndPercentage,
                    HighEndACEValue = request.HighEndACEValue,
                    LowEndACEValue = request.LowEndACEValue,
                    HighEndAveragePremium = request.HighEndAveragePremium,
                    LowEndAveragePremium = request.LowEndAveragePremium,
                    HighEndNoOfCases = request.HighEndNoOfCases,
                    LowEndNoOfCases = request.LowEndNoOfCases,
                    Remarks = request.Remarks,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateBudgetStrategy(obj);

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
        [Route("strategy-get-by-filter")]
        public IHttpActionResult GetBudgetstrategyByFilter([FromBody] BudgetStrategyFilter request)
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
                var resultList = oCaliphService.GetBudgetStrategyByFilter(request, request.PageSize, request.PageNumber);
                if (resultList != null)
                {
                    response.ItemCount = oCaliphService.GetBudgetStrategyByFilter(request, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo).Count;
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

        #region Budget - Target Appt
        [HttpPost]
        [Route("target-appt-update")]
        public IHttpActionResult UpdateTargetApptClosingRatio([FromBody] BudgetRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || request.BudgetId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_BUGDET_ID;
                    response.StatusMsg = APIStatusCode.INVALID_BUGDET_MSG;
                    return Ok(response);
                }

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                var oCaliphService = new CaliphService();
                var oBudgetEnt = oCaliphService.GetBudgetById(request.BudgetId);
                if (oBudgetEnt == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_BUGDET_ID;
                    response.StatusMsg = APIStatusCode.INVALID_BUGDET_MSG;
                    return Ok(response);
                }

                var obj = new BudgetEnt
                {
                    BudgetId = request.BudgetId,
                    TargetApptClosingRatio = request.TargetApptClosingRatio,
                    TargetApptCallRatio= request.TargetApptCallRatio,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateTargetApptClosingRatio(obj);

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
        #endregion

    }
}