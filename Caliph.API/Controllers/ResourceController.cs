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
    [RoutePrefix("api/v1/Resource")]
    public class ResourceController :BaseController
    {
        // GET: Resource
        /// <summary>
        /// Get client Deal id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("get")]
        //public IHttpActionResult GetResourceUsernameRequest ([FromBody]ResourceUserRequest request)
        //{
        //    var response = new ResponseApiModel();
        //    string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

        //    try
        //    {
        //        if (request == null || string.IsNullOrEmpty(request.Username))
        //        {
        //            response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
        //            response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;
        //            return Ok(response);
        //        }

        //        var oCaliphService = new CaliphService();
        //        var oUsersEnt = oCaliphService.GetResourceByUsername(request);
        //        if (oUsersEnt == null)
        //        {
        //            response.StatusCode = APIStatusCode.INVALID_SYSTEM_USER_CODE;
        //            response.StatusMsg = APIStatusCode.INVALID_SYSTEM_USER_MSG;
        //            return Ok(response);
        //        }

               

          
        //        response.data = oUsersEnt;
        //        response.StatusCode = APIStatusCode.SUCC_CODE;
        //        response.StatusMsg = APIStatusCode.SUCC_MSG;

                
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = APIStatusCode.FAIL_CODE;
        //        response.StatusMsg = APIStatusCode.FAIL_MSG;

        //        LogHelper.Error(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, ex.ToString()));
        //    }
        //    finally
        //    {
        //        functionParam += "|Response: " + new JavaScriptSerializer().Serialize(response);
        //        LogHelper.Info(LogHelper.LogFormat(MethodBase.GetCurrentMethod().Name, functionParam, "", false));
        //    }

        //    return Ok(response);
        //}




        [HttpPost]
        [Route("add")]

        public IHttpActionResult AddResource([FromBody] ResourceFilterRequest request)
        {
            var response = new ResponseApiModel();
            string functionParam = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");

            try
            {
                if (request == null || string.IsNullOrEmpty(request.Name))
                {
                    response.StatusCode = APIStatusCode.INVALID_PARAM_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_PARAM_MSG;

                    // test 
                    return Ok(response);
                }

                var oCaliphService = new CaliphService();

                //var checkuplinecheckuplineEnt = new ResourceUserRequest() { Username = request.UserName };
                //var oUplineUsersEnt = oCaliphService.GetResourceByUsername(checkuplinecheckuplineEnt);
                //if (oUplineUsersEnt == null)
                //{
                //    response.StatusCode = APIStatusCode.INVALID_UPLINE_USERNAME_CODE;
                //    response.StatusMsg = APIStatusCode.INVALID_UPLINE_USERNAME_MSG;
                //    return Ok(response);
                //}


                //var oUsersEnt = oCaliphService.GetResourceByFilter(null, request.Name, null, null);
                //if (oUsersEnt == null)
                //{
                //    response.StatusCode = APIStatusCode.INVALID_SYSTEM_USER_CODE;
                //    response.StatusMsg = APIStatusCode.INVALID_SYSTEM_USER_MSG;
                //    return Ok(response);
                //}

                #region bind function param
                JavaScriptSerializer js = new JavaScriptSerializer();
                functionParam += new JavaScriptSerializer().Serialize(request);
                #endregion

                


                //var oResource = oCaliphService.GetResourceByFilter(null, request.Name, request.CreatedDateFrom, request.CreatedDateTo);
                //if (oResource == null)
                //{
                //    response.StatusCode = APIStatusCode.INVALID_RESOURCE_CODE;
                //    response.StatusMsg = APIStatusCode.INVALID_RESOURCE_MSG;
                //    return Ok(response);
                //}
                //    var oCaliphService = new CaliphService();
             


                // 
                //                 
                // */

                var obj = new ResourcesEnt
                {
                  
                    Name = request.Name,
                    Url= request.Url,
                    ResourceId = request.ResourceId,
                    //UserId = request.UserId,
                    UserName = request.UserName,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    CreatedBy = request.CreatedBy
                };

                var succ = oCaliphService.AddResource(obj);

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
        [Route("update")]
        public IHttpActionResult UpdateResource([FromBody] ResourceRequest request)
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



                if (String.IsNullOrEmpty(request.UpdatedBy))
                {
                    response.StatusCode = APIStatusCode.INVALID_UPDATED_BY_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_UPDATED_BY_MSG;
                    return Ok(response);
                }
                #endregion

                var oCaliphService = new CaliphService();
                var oResource = oCaliphService.GetResourceById(request.ResourceId);
                 if (oResource == null)
                 {
                    response.StatusCode = APIStatusCode.INVALID_RESOURCE_CODE;
                     response.StatusMsg = APIStatusCode.INVALID_RESOURCE_MSG;
                     return Ok(response);
                 }

                 
                var oUpdateResource = new ResourcesEnt
                {
                    ResourceId = request.ResourceId,
                    Name = request.Name,
                    Url = request.Url,
                    UserName = request.UserName,
                    StatusId = (long)MasterDataEnum.Status_Active,
                    UpdatedBy = request.UpdatedBy
                };

                var succ = oCaliphService.UpdateResource(oUpdateResource);
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
        }//update


        
        [HttpPost]
        [Route("get-by-Resource-id")]
        public IHttpActionResult GetResourceById([FromBody] ResourceFilterRequest request)
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
                var oResource = oCaliphService.GetResourceById(request.ResourceId);
                if (oResource != null)
                {
                    response.data = oResource;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_RESOURCE_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_RESOURCE_MSG;
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
        public IHttpActionResult UpdatResoruceStatusInactive([FromBody] ResourceRequest request)
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

                if (request.ResourceId <= 0)
                {
                    response.StatusCode = APIStatusCode.INVALID_RESOURCE_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_RESOURCE_MSG;
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
                var oResource = oCaliphService.GetResourceById(request.ResourceId);
                if (oResource == null)
                {
                    response.StatusCode = APIStatusCode.INVALID_RESOURCE_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_RESOURCE_MSG;
                    return Ok(response);
                }

                if (oResource.StatusId == (long)MasterDataEnum.Status_Inactive)
                {
                    response.StatusCode = APIStatusCode.ALREADY_DELETED_BY_CODE;
                    response.StatusMsg = APIStatusCode.ALREADY_DELETED_BY_MSG;
                    return Ok(response);
                }

                var succ = oCaliphService.UpdateResourceStatus(request.ResourceId, (long)MasterDataEnum.Status_Inactive, request.UpdatedBy, oResource.ResourceId);
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
        [Route("get-by-Resource-filter")]
        public IHttpActionResult GetResourceByFilter ([FromBody] ResourceFilterRequest request)
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
                var oResource = oCaliphService.GetResourceByFilter(null, request.Name, request.CreatedDateFrom, request.CreatedDateTo);


                if (oResource != null)
                {
                    response.ItemCount = oCaliphService.GetResourceByFilter(null, request.UserName, request.CreatedDateFrom, request.CreatedDateTo).Count;
                    response.data = oResource;

                    response.StatusCode = APIStatusCode.SUCC_CODE;
                    response.StatusMsg = APIStatusCode.SUCC_MSG;
                }
                else
                {
                    response.StatusCode = APIStatusCode.INVALID_RESOURCE_CODE;
                    response.StatusMsg = APIStatusCode.INVALID_RESOURCE_MSG;
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


    }//resource controller
}