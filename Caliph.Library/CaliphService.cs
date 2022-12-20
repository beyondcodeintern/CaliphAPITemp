using Caliph.Library.Helper;
using Caliph.Library.Models;
using Caliph.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Caliph.Library
{
    public class CaliphService
    {
        private CaliphRepo _repo;

        public CaliphService()
        {
            _repo = new CaliphRepo(ConfigHelper.MSSQL_ConStr);
        }

        public List<MasterDatasEnt> GetMasterDataByMasterId(long masterId)
        {
            return _repo.GetMasterDataByMasterId(masterId);
        }

        public List<ActivityPointsEnt> GetActivityPoint()
        {
            return _repo.GetActivityPoint();
        }
        public bool AddClient(ClientsEnt oClient)
        {
            return _repo.AddClient(oClient);
        }

        public List<ClientDetailsEnt> GetClientAll()
        {
            return _repo.GetClientAll(null);
        }

        public ClientDetailsEnt GetClientById(long? clientId, string createdBy)
        {
            var oClientDetailsEnt = new ClientDetailsEnt();
            var oClientList = _repo.GetClientByFilter(clientId, null, null, null, null, null, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo, false, createdBy, null, null);
            if (oClientList.Count > 0)
            {
                foreach (var item in oClientList)
                {
                    oClientDetailsEnt = item;
                }
            }
            else
            {
                oClientDetailsEnt = null;
            }

            return oClientDetailsEnt;
        }

        public ClientDetailsEnt GetDuplicateClientHpByAgent(string contact, string createdBy)
        {
            var oClientDetailsEnt = new ClientDetailsEnt();
            var oClientList = _repo.GetClientByFilter(null, null, null, null, null, null, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo, false, createdBy, null, null, contact);
            if (oClientList.Count > 0)
            {
                foreach (var item in oClientList)
                {
                    oClientDetailsEnt = item;
                }
            }
            else
            {
                oClientDetailsEnt = null;
            }

            return oClientDetailsEnt;
        }

        /// <summary>
        /// Filter KIV status
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="statusId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<ClientDetailsEnt> GetClientByFilter(long? clientId, long? statusId, DateTime? KIVDateFrom, DateTime? KIVDateTo, DateTime? CreatedDateFrom, DateTime? CreatedDateTo, long pageSize, long pageNumber, bool filterKIV, string createdBy, string statusIdList, string name = "", string contact=null)
        {
            return _repo.GetClientByFilter(clientId, statusId, KIVDateFrom, KIVDateTo, CreatedDateFrom, CreatedDateTo, pageSize, pageNumber, filterKIV, createdBy, statusIdList, name, contact);
        }

        public UsersEnt GetSystemUserByUsername(SystemUserRequest request)
        {
            var obj = new UsersEnt();
            var oList = _repo.GetSystemUserByUsername(request);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }


        public List<UsersMenuEnt> GetMenuByUser(string username)
        {
            return _repo.GetMenuByUser(username);
        }

        public List<UsersEnt> GetSystemUserByFilter(SystemUserFilter request, long pageSize, long pageNumber)
        {
            return _repo.GetSystemUserByFilter(request, pageSize, pageNumber);
        }

        public List<UsersStaffEnt> GetSystemStaffByFilter(SystemUserFilter request, long pageSize, long pageNumber)
        {
            return _repo.GetSystemStaffByFilter(request, pageSize, pageNumber);
        }

        public bool UpdateClientStatus(long clientId, long statusId, string updatedBy, bool IsRevert = false)
        {
            return _repo.UpdateClientStatus(clientId, statusId, updatedBy, IsRevert);
        }

        public bool UpdateClientBasicInfo(ClientsEnt oClient)
        {
            return _repo.UpdateClientBasicInfo(oClient);
        }

        public bool UpdateClientRelationshipInfo(ClientsEnt oClient)
        {
            return _repo.UpdateClientRelationshipInfo(oClient);
        }

        #region Client Policy        
        public bool AddClientPolicy(ClientPolicyEnt obj)
        {
            return _repo.AddClientPolicy(obj);
        }

        public bool UpdateClientPolicy(ClientPolicyEnt obj)
        {
            return _repo.UpdateClientPolicy(obj);
        }

        public bool UpdateClientPolicyStatus(long clientPolicyId, long statusId, string updatedBy)
        {
            return _repo.UpdateClientPolicyStatus(clientPolicyId, statusId, updatedBy);
        }

        public List<ClientPolicyEnt> GetClientPolicyByFilter(long? clientId, long? clientPolicyId, long? statusId, long pageSize, long pageNumber)
        {
            return _repo.GetClientPolicyByFilter(clientId, clientPolicyId, statusId, pageSize, pageNumber);
        }

        public ClientPolicyEnt GetClientPolicyById(long? clientPolicyId)
        {
            var obj = new ClientPolicyEnt();
            var oList = _repo.GetClientPolicyByFilter(null, clientPolicyId, null, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Client Family
        public bool AddClientFamily(ClientFamilyEnt obj)
        {
            return _repo.AddClientFamily(obj);
        }

        public bool UpdateClientFamily(ClientFamilyEnt obj)
        {
            return _repo.UpdateClientFamily(obj);
        }

        public bool UpdateClientFamilyStatus(long clientFamilyId, long statusId, string updatedBy)
        {
            return _repo.UpdateClientFamilyStatus(clientFamilyId, statusId, updatedBy);
        }

        public List<ClientFamilyEnt> GetClientFamilyByFilter(long? clientId, long? clientFamilyId, long? statusId, long pageSize, long pageNumber)
        {
            return _repo.GetClientFamilyByFilter(clientId, clientFamilyId, statusId, pageSize, pageNumber);
        }

        public ClientFamilyEnt GetClientFamilyById(long? clientFamilyId)
        {
            var obj = new ClientFamilyEnt();
            var oList = _repo.GetClientFamilyByFilter(null, clientFamilyId, null, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Client Deal
        public bool AddClientDeal(ClientDealEnt obj)
        {
            return _repo.AddClientDeal(obj);
        }

        public bool UpdateClientDeal(ClientDealEnt obj)
        {
            return _repo.UpdateClientDeal(obj);
        }

        public bool UpdateClientDealStatus(long clientDealId, long statusId, string updatedBy, long clientId)
        {
            bool IsClientConfirmed = false;
            if (statusId == (long)MasterDataEnum.Status_Closed)
            {
                IsClientConfirmed = true;
            }
            return _repo.UpdateClientDealStatus(clientDealId, statusId, updatedBy, IsClientConfirmed, clientId);
        }

        public List<ClientDealEnt> GetClientDealByFilter(long? clientId, long? clientDealId, long? statusId, long? dealTitleId, string name, string clientName, string createdBy, string clientCreatedBy, DateTime? createdDateFrom, DateTime? createdDateTo, long pageSize, long pageNumber)
        {
            return _repo.GetClientDealByFilter(clientId, clientDealId, statusId, dealTitleId, name, clientName, createdBy, clientCreatedBy, createdDateFrom, createdDateTo, pageSize, pageNumber);
        }

        public ClientDealEnt GetClientDealById(long? clientDealId, string updatedBy = null)
        {
            var obj = new ClientDealEnt();
            var oList = _repo.GetClientDealByFilter(null, clientDealId, null, null, null, null, null, updatedBy, null, null, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Client Deal's Activity
        public long AddClientDealActivity(ClientActivityEnt obj)
        {
            return _repo.AddClientDealActivity(obj);
        }

        public bool UpdateClientDealActivity(ClientActivityEnt obj)
        {
            return _repo.UpdateClientDealActivity(obj);
        }

        public bool UpdateClientDealActivityStatus(long clientDealActivityId, long statusId, int points, string updatedBy)
        {
            return _repo.UpdateClientDealActivityStatus(clientDealActivityId, statusId, points, updatedBy);
        }

        public List<ClientActivityEnt> GetClientDealActivityByFilter(long? clientId, long? clientDealActivityId, long? dealTitleId, long? clientDealId, long? statusId, DateTime? activityStartDate, DateTime? activityEndDate, string createdBy, long? clientDealStatusId, long pageSize, long pageNumber)
        {
            return _repo.GetClientDealActivityByFilter(clientId, clientDealActivityId, dealTitleId, clientDealId, statusId, activityStartDate, activityEndDate, createdBy, clientDealStatusId, pageSize, pageNumber);
        }

        public ClientActivityEnt GetClientDealActivityById(long? clientDealActivityId, string createdBy = null)
        {
            var obj = new ClientActivityEnt();
            var oList = _repo.GetClientDealActivityByFilter(null, clientDealActivityId, null, null, null, null, null, createdBy, null, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Client Lead        
        public bool AddClientLead(ClientLeadEnt obj)
        {
            return _repo.AddClientLead(obj);
        }

        public bool UpdateClientLead(ClientLeadEnt obj)
        {
            return _repo.UpdateClientLead(obj);
        }

        public bool UpdateClientLeadStatus(long clientLeadId, long statusId, string updatedBy)
        {
            return _repo.UpdateClientLeadStatus(clientLeadId, statusId, updatedBy);
        }

        public List<ClientLeadEnt> GetClientLeadByFilter(long? clientLeadId, long? clientDealActivityId, long? statusId, long pageSize, long pageNumber)
        {
            return _repo.GetClientLeadByFilter(clientLeadId, clientDealActivityId, statusId, pageSize, pageNumber);
        }

        public ClientLeadEnt GetClientLeadById(long? clientLeadId)
        {
            var obj = new ClientLeadEnt();
            var oList = _repo.GetClientLeadByFilter(clientLeadId, null, null, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public void CheckRefLeadEntitlement(long clientDealActivityId)
        {
            int points = 0;

            var oClientLeadList = _repo.GetClientLeadByFilter(null, clientDealActivityId, (long)MasterDataEnum.Status_ConvertedToClient, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);

            var objDealActity = _repo.GetClientDealActivityByFilter(null, clientDealActivityId, null, null, null, null, null, null, null, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);

            if (oClientLeadList.Count >= 3)
            {
                points = objDealActity[0].PointSetting;
            }

            int existingPoint = objDealActity[0].Points;
            if (points != existingPoint)
            {
                _repo.UpdateClientDealActivityPoint(clientDealActivityId, points);
            }
        }

        #endregion

        public List<ClientKIVRevertHistory> GetClientKIVRevertHistoryByFilter(long? clientId, string name, DateTime? KIVDateFrom, DateTime? KIVDateTo, string createdBy, long pageSize, long pageNumber)
        {
            return _repo.GetClientKIVRevertHistoryByFilter(clientId, name, KIVDateFrom, KIVDateTo, createdBy, pageSize, pageNumber);
        }

        public List<ClientSummaryEnt> GetClientSummaryByFilter(DateTime? createdDateFrom, DateTime? createdDateTo, string createdBy)
        {
            return _repo.GetClientSummaryByFilter(createdDateFrom, createdDateTo, createdBy);
        }
        public List<ClientSummaryEnt> GetAgentReruitSummaryByFilter(DateTime? createdDateFrom, DateTime? createdDateTo, string createdBy)
        {
            return _repo.GetAgentReruitSummaryByFilter(createdDateFrom, createdDateTo, createdBy);
        }

        public List<AgentEnt> GetSystemUserByFilter(string username, long? roleId, long? uplineUserId)
        {
            return _repo.GetSystemUserByFilter(username, roleId, uplineUserId);
        }

        #region Client Activity Review
        public long AddActivityReview(ActivityReviewEnt obj)
        {
            return _repo.AddActivityReview(obj);
        }

        public bool UpdateActivityReview(ActivityReviewEnt obj)
        {
            return _repo.UpdateActivityReview(obj);
        }

        public List<ActivityReviewEnt> GetActivityReviewByFilter(ActivityReviewFilter obj)
        {
            return _repo.GetActivityReviewByFilter(obj, obj.PageSize, obj.PageNumber);
        }

        public ActivityReviewEnt GetActivityReviewById(long? activityReviewId)
        {
            var filter = new ActivityReviewFilter { ActivityReviewId = activityReviewId };
            var obj = new ActivityReviewEnt();
            var oList = _repo.GetActivityReviewByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region User Activity
        public long AddUserActivity(UserActivityEnt obj)
        {
            return _repo.AddUserActivity(obj);
        }

        public bool UpdateUserActivity(UserActivityEnt obj)
        {
            return _repo.UpdateUserActivity(obj);
        }

        public List<UserActivityEnt> GetUserActivityByFilter(UserActivityFilter obj)
        {
            return _repo.GetUserActivityByFilter(obj, obj.PageSize, obj.PageNumber);
        }

        public UserActivityEnt GetUserActivityById(long? userActivityId)
        {
            var filter = new UserActivityFilter { UserActivityId = userActivityId };
            var obj = new UserActivityEnt();
            var oList = _repo.GetUserActivityByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region users
        public long AddUsers(UsersEnt obj)
        {
            return _repo.AddUsers(obj);
        }

        public bool UpdateUserStatus(string username, long statusId, string updatedBy)
        {
            return _repo.UpdateUserStatus(username, statusId, updatedBy);
        }

        public bool UpdateUserPW(string username, string pw, string updatedBy)
        {
            return _repo.UpdateUserPW(username, pw, updatedBy);
        }

        public bool ConvertOne2OneAgent(string username, string newUsername, long roleId)
        {
            return _repo.ConvertOne2OneAgent(username, newUsername, roleId);
        }
        #endregion

        #region Announcement
        public long AddAnnouncement(AnnouncementEnt obj, List<long> userIdList)
        {
            #region user id list format
            string userIdListFormat = "";

            if (userIdList != null)
            {
                userIdListFormat = IdListToString(userIdList);
            }
            #endregion

            return _repo.AddAnnouncement(obj, userIdListFormat);
        }

        public bool UpdateAnnouncement(AnnouncementEnt obj, List<long> userIdList)
        {
            #region user id list format
            string userIdListFormat = "";

            if (userIdList != null)
            {
                userIdListFormat = IdListToString(userIdList);
            }
            #endregion

            return _repo.UpdateAnnouncement(obj, userIdListFormat);
        }

        public bool UpdateAnnouncementStatus(long announcementId, long statusId, string updatedBy)
        {
            return _repo.UpdateAnnouncementStatus(announcementId, statusId, updatedBy);
        }
        public List<AnnouncementEnt> GetAnnouncementByFilter(AnnouncementFilter obj, long pageSize, long pageNumber)
        {
            List<AnnouncementEnt> returnList = new List<AnnouncementEnt>();

            var resultList = _repo.GetAnnouncementByFilter(obj, pageSize, pageNumber);

            var resultWithUser = _repo.GetAnnouncementByFilter_UserList(obj, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo, true);
            foreach (var item in resultList)
            {
                if (item.AnnouncementTypeId == (long)AnnouncementType.specified_user)
                {
                    if (obj.UserId != null)
                    {
                        var hasUser = resultWithUser.Where(x => x.AnnouncementId.Equals(item.AnnouncementId) && x.UserId.Equals(obj.UserId)).Count();
                        if (hasUser > 0)
                        {
                            item.UserList = resultWithUser.Where(x => x.AnnouncementId.Equals(item.AnnouncementId)).ToList();
                            returnList.Add(item);
                        }
                        else
                        {
                            item.UserList = null;
                        }
                    }
                    else
                    {
                        item.UserList = resultWithUser.Where(x => x.AnnouncementId.Equals(item.AnnouncementId)).ToList();
                        returnList.Add(item);
                    }
                }
                else
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }

        public AnnouncementEnt GetAnnouncementById(long? announcementId)
        {
            var filter = new AnnouncementFilter { AnnouncementId = announcementId };
            var obj = new AnnouncementEnt();
            var oList = _repo.GetAnnouncementByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        public string IdListToString(List<long> idList)
        {
            string result = "";
            foreach (var item in idList)
            {
                result += $"{item.ToString()},";
            }

            result = result.Remove(result.Length - 1, 1);

            return result;
        }

        public string ObjectListToString(List<EventEnt> idList)
        {
            string result = "";
            foreach (var item in idList)
            {
                result += $"{item.EventId},";
            }

            result = result.Remove(result.Length - 1, 1);

            return result;
        }

        public bool IsValidActivityReviewType(MasterEnum masterEnum, long value)
        {
            var oList = _repo.GetMasterDataByMasterId((long)masterEnum);

            if (oList.Where(x => x.MasterDataId.Equals(value)).Count() > 0)
            {
                return true;
            }

            return false;
        }

        #region Budget
        public long AddBudget(BudgetEnt obj)
        {
            return _repo.AddBudget(obj);
        }

        public bool UpdateBudget(BudgetEnt obj)
        {
            return _repo.UpdateBudget(obj);
        }

        public List<BudgetEnt> GetBudgetByFilter(BudgetFilter obj, bool withBudgetGroup)
        {
            var oBudgetList = new List<BudgetEnt>();
            oBudgetList = _repo.GetBudgetByFilter(obj, obj.PageSize, obj.PageNumber);

            if (withBudgetGroup)
            {
                foreach (var item in oBudgetList)
                {
                    var filter = new BudgetFilter() { BudgetId = item.BudgetId, BudgetGroupUserId = obj.BudgetGroupUserId };
                    item.BudgetGroupList = _repo.GetBudgetGroupByFilter(filter);
                }
            }

            return oBudgetList;
        }

        public BudgetGroupEnt GetBudgetGroupById(long? budgetGroupId)
        {

            var filter = new BudgetFilter { BudgetGroupId = budgetGroupId };
            var obj = new BudgetGroupEnt();
            var oList = _repo.GetBudgetGroupByFilter(filter);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public BudgetEnt GetBudgetById(long? budgetId)
        {
            var filter = new BudgetFilter { BudgetId = budgetId };
            var obj = new BudgetEnt();
            var oList = _repo.GetBudgetByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Budget - Target Appt
        public bool UpdateTargetApptClosingRatio(BudgetEnt obj)
        {
            return _repo.UpdateTargetApptClosingRatio(obj);
        }
        #endregion

        public bool UpdateBudgetProportion(BudgetProportionEnt obj)
        {
            return _repo.UpdateBudgetProportion(obj);
        }

        public List<ClientLeadReport> GetClientLeadReport(ClientLeadReportFilter obj)
        {
            return _repo.GetClientLeadReport(obj);
        }

        #region Budget group
        public long AddBudgetGroup(BudgetGroupEnt obj)
        {
            return _repo.AddBudgetGroup(obj);
        }

        public bool UpdateBudgetGroup(BudgetGroupEnt obj)
        {
            return _repo.UpdateBudgetGroup(obj);
        }

        public bool UpdateStatusBudgetGroup(long budgetGroupId, long statusId, string updatedBy)
        {
            return _repo.UpdateStatusBudgetGroup(budgetGroupId, statusId, updatedBy);
        }
        #endregion

        #region Monthly budget
        public long AddBudgetMonthly(BudgetMonthlyEnt obj)
        {
            return _repo.AddBudgetMonthly(obj);
        }

        public bool UpdateBudgetMonthly(BudgetMonthlyEnt obj)
        {
            return _repo.UpdateBudgetMonthly(obj);
        }

        public List<BudgetMonthlyEnt> GetBudgetMonthlyByFilter(BudgetMonthlyFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetBudgetMonthlyByFilter(obj, pageSize, pageNumber);
        }

        public bool UpdateStatusBudgetMonthly(long budgetMonthlyId, long statusId, string updatedBy)
        {
            return _repo.UpdateStatusBudgetMonthly(budgetMonthlyId, statusId, updatedBy);
        }

        public BudgetMonthlyEnt GetBudgetMonthlyById(long? budgetMonthlyId)
        {
            var filter = new BudgetMonthlyFilter { BudgetMonthlyId = budgetMonthlyId };
            var obj = new BudgetMonthlyEnt();
            var oList = _repo.GetBudgetMonthlyByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region budget strategy
        public long AddBudgetStrategy(BudgetStrategyEnt obj)
        {
            return _repo.AddBudgetStrategy(obj);
        }

        public bool UpdateBudgetStrategy(BudgetStrategyEnt obj)
        {
            return _repo.UpdateBudgetStrategy(obj);
        }

        public List<BudgetStrategyEnt> GetBudgetStrategyByFilter(BudgetStrategyFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetBudgetStrategyByFilter(obj, pageSize, pageNumber);
        }

        public BudgetStrategyEnt GetBudgetStrategyById(long? budgetStrategyId)
        {
            var filter = new BudgetStrategyFilter { BudgetStrategyId = budgetStrategyId };
            var obj = new BudgetStrategyEnt();
            var oList = _repo.GetBudgetStrategyByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Agent Recruit
        public long AddAgentRecruit(AgentRecruitEnt obj)
        {
            return _repo.AddAgentRecruit(obj);
        }

        public bool UpdateAgentRecruit(AgentRecruitEnt obj)
        {
            return _repo.UpdateAgentRecruit(obj);
        }

        public bool UpdateAgentRecruitStatus(AgentRecruitEnt obj)
        {
            return _repo.UpdateAgentRecruitStatus(obj);
        }

        public List<AgentRecruitEnt> GetAgentRecruitByFilter(AgentRecruitFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetAgentRecruitByFilter(obj, pageSize, pageNumber);
        }

        public AgentRecruitEnt GetAgentRecruitById(long? agentRecruitId)
        {
            var filter = new AgentRecruitFilter { AgentRecruitId = agentRecruitId };
            var obj = new AgentRecruitEnt();
            var oList = _repo.GetAgentRecruitByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region MyRegion
        public long AddAgentRecruitTrack(AgentRecruitTrackEnt obj)
        {
            return _repo.AddAgentRecruitTrack(obj);
        }

        public bool UpdateAgentRecruitTrack(AgentRecruitTrackEnt obj)
        {
            return _repo.UpdateAgentRecruitTrack(obj);
        }

        public List<AgentRecruitTrackEnt> GetAgentRecruitTrackByFilter(AgentRecruitTrackFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetAgentRecruitTrackByFilter(obj, pageSize, pageNumber);
        }

        public AgentRecruitTrackEnt GetAgentRecruitTrackById(long? AgentRecruitTrackId)
        {
            var filter = new AgentRecruitTrackFilter { AgentRecruitTrackId = AgentRecruitTrackId };
            var obj = new AgentRecruitTrackEnt();
            var oList = _repo.GetAgentRecruitTrackByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Agent Activity
        public long AddAgentActivity(AgentActivityEnt obj)
        {
            return _repo.AddAgentActivity(obj);
        }

        public bool UpdateAgentActivity(AgentActivityEnt obj)
        {
            return _repo.UpdateAgentActivity(obj);
        }

        public List<AgentActivityEnt> GetAgentActivityByFilter(AgentActivityFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetAgentActivityByFilter(obj, pageSize, pageNumber);
        }

        public AgentActivityEnt GetAgentActivityById(long? agentActivityId)
        {
            var filter = new AgentActivityFilter { AgentActivityId = agentActivityId };
            var obj = new AgentActivityEnt();
            var oList = _repo.GetAgentActivityByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Agent Lead        
        public long AddAgentLead(AgentLeadEnt obj)
        {
            return _repo.AddAgentLead(obj);
        }

        public bool UpdateAgentLead(AgentLeadEnt obj)
        {
            return _repo.UpdateAgentLead(obj);
        }

        public bool UpdateAgentLeadStatus(long agentLeadId, long statusId, string updatedBy)
        {
            return _repo.UpdateAgentLeadStatus(agentLeadId, statusId, updatedBy);
        }

        public List<AgentLeadEnt> GetAgentLeadByFilter(AgentLeadFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetAgentLeadByFilter(obj, pageSize, pageNumber);
        }

        public AgentLeadEnt GetAgentLeadById(long? agentLeadId)
        {
            var obj = new AgentLeadEnt();
            var filter = new AgentLeadFilter { AgentLeadId = agentLeadId };

            var oList = _repo.GetAgentLeadByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public void CheckAgentRefLeadEntitlement(long agentActivityId)
        {
            int points = 0;
            var filter = new AgentLeadFilter { AgentActivityId = agentActivityId, StatusId = (long)MasterDataEnum.Status_ConvertedToClient };
            var oAgentLeadList = _repo.GetAgentLeadByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);


            var filterAgentActivity = new AgentActivityFilter { AgentActivityId = agentActivityId };
            var objAgentActity = _repo.GetAgentActivityByFilter(filterAgentActivity, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);

            if (oAgentLeadList.Count >= 3)
            {
                points = objAgentActity[0].PointSetting;
            }

            int existingPoint = objAgentActity[0].Points;
            if (points != existingPoint)
            {
                _repo.UpdateAgentActivityPoint(agentActivityId, points);
            }
        }
        #endregion

        #region Agent Simulator
        public long AddAgentSimulator(AgentSimulatorEnt obj)
        {
            return _repo.AddAgentSimulator(obj);
        }

        public bool UpdateAgentSimulator(AgentSimulatorEnt obj)
        {
            return _repo.UpdateAgentSimulator(obj);
        }

        public List<AgentSimulatorEnt> GetAgentSimulatorByFilter(AgentSimulatorFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetAgentSimulatorByFilter(obj, pageSize, pageNumber);
        }

        public AgentSimulatorEnt GetAgentSimulatorById(long? AgentSimulatorId)
        {
            var filter = new AgentSimulatorFilter { AgentSimulatorId = AgentSimulatorId };
            var obj = new AgentSimulatorEnt();
            var oList = _repo.GetAgentSimulatorByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        #region Event
        public long AddEvent(EventEnt obj)
        {
            return _repo.AddEvent(obj);
        }

        public bool UpdateEvent(EventEnt obj)
        {
            return _repo.UpdateEvent(obj);
        }

        public List<EventEnt> GetEventByFilter(EventFilter obj, long pageSize, long pageNumber)
        {
            List<EventEnt> eventList = _repo.GetEventByFilter(obj, pageSize, pageNumber);

            if (eventList.Count > 0)
            {
                var eventIdList = ObjectListToString(eventList);

                var eventDateFilter = new EventDateFilter(); // default value
                List<EventDateEnt> eventDateList = GetEventDateByFilter(eventDateFilter, eventIdList, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
                List<EventRoleEnt> eventRoleList = GetEventRoleByFilter(eventDateFilter, eventIdList);
                foreach (var item in eventList)
                {
                    item.EventDateList = eventDateList.Where(x => x.EventId.Equals(item.EventId)).ToList();
                    item.EventRoleList = eventRoleList.Where(x => x.EventId.Equals(item.EventId)).ToList();
                }
            }

            return eventList;
        }

        public EventEnt GetEventById(long? EventId)
        {
            var filter = new EventFilter { EventId = EventId };
            var obj = new EventEnt();
            var oList = _repo.GetEventByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public bool UpdateEventStatus(long eventId, long statusId, string updatedBy)
        {
            return _repo.UpdateEventStatus(eventId, statusId, updatedBy);
        }

        public List<UpcomingEventEnt> GetUpcomingEventByFilter(EventFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetUpcomingEventByFilter(obj, pageSize, pageNumber);
        }
        #endregion

        #region Event Date 
        public long AddEventDate(EventDateEnt obj)
        {
            return _repo.AddEventDate(obj);
        }

        public bool UpdateEventDate(EventDateEnt obj)
        {
            return _repo.UpdateEventDate(obj);
        }

        public EventDateEnt GetEventDateById(long? EventDateId)
        {
            var filter = new EventDateFilter { EventDateId = EventDateId };
            var obj = new EventDateEnt();
            var oList = _repo.GetEventDateByFilter(filter, "", ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public bool UpdateEventDateStatus(long eventDateId, long statusId, string updatedBy)
        {
            return _repo.UpdateEventDateStatus(eventDateId, statusId, updatedBy);
        }

        public List<EventDateEnt> GetEventDateByFilter(EventDateFilter obj, string eventIdList, long pageSize, long pageNumber)
        {
            return _repo.GetEventDateByFilter(obj, eventIdList, pageSize, pageNumber);
        }

        public List<EventRoleEnt> GetEventRoleByFilter(EventDateFilter obj, string eventIdList)
        {
            return _repo.GetEventRoleByFilter(obj, eventIdList);
        }
        #endregion

        #region Event Attachment 
        public long AddEventAttachment(EventAttachmentEnt obj)
        {
            return _repo.AddEventAttachment(obj);
        }

        public bool UpdateEventAttachment(EventAttachmentEnt obj)
        {
            return _repo.UpdateEventAttachment(obj);
        }

        public EventAttachmentEnt GetEventAttachmentById(long? EventAttachmentId)
        {
            var filter = new EventAttachmentFilter { EventAttachmentId = EventAttachmentId };
            var obj = new EventAttachmentEnt();
            var oList = _repo.GetEventAttachmentByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public bool UpdateEventAttachmentStatus(long EventAttachmentId, long statusId, string updatedBy)
        {
            return _repo.UpdateEventAttachmentStatus(EventAttachmentId, statusId, updatedBy);
        }

        public List<EventAttachmentEnt> GetEventAttachmentByFilter(EventAttachmentFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetEventAttachmentByFilter(obj, pageSize, pageNumber);
        }
        #endregion

        #region User Event
        public long AddUserEvent(UserEventEnt obj)
        {
            return _repo.AddUserEvent(obj);
        }

        public bool UpdateUserEvent(UserEventEnt obj)
        {
            return _repo.UpdateUserEvent(obj);
        }

        public UserEventEnt GetUserEventById(long? UserEventId)
        {
            var filter = new UserEventFilter { UserEventId = UserEventId };
            var obj = new UserEventEnt();
            var oList = _repo.GetUserEventByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public bool UpdateUserEventStatus(long UserEventId, long statusId, string updatedBy)
        {
            return _repo.UpdateUserEventStatus(UserEventId, statusId, updatedBy);
        }

        public List<UserEventEnt> GetUserEventByFilter(UserEventFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetUserEventByFilter(obj, pageSize, pageNumber);
        }

        public bool UpdateUserEventAttendance(long UserEventId, long attendanceId, string updatedBy)
        {
            return _repo.UpdateUserEventAttendance(UserEventId, attendanceId, updatedBy);
        }
        #endregion

        #region Event Attachment 
        public long AddUserEventPayment(UserEventPaymentEnt obj)
        {
            return _repo.AddUserEventPayment(obj);
        }

        public bool UpdateUserEventPayment(UserEventPaymentEnt obj)
        {
            return _repo.UpdateUserEventPayment(obj);
        }

        public UserEventPaymentEnt GetUserEventPaymentById(long? UserEventPaymentId)
        {
            var filter = new UserEventPaymentFilter { UserEventPaymentId = UserEventPaymentId };
            var obj = new UserEventPaymentEnt();
            var oList = _repo.GetUserEventPaymentByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        /*
        public bool UpdateUserEventPaymentStatus(long UserEventPaymentId, long statusId, string updatedBy)
        {
            return _repo.UpdateUserEventPaymentStatus(UserEventPaymentId, statusId, updatedBy);
        }
        */

        public List<UserEventPaymentEnt> GetUserEventPaymentByFilter(UserEventPaymentFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetUserEventPaymentByFilter(obj, pageSize, pageNumber);
        }
        #endregion

        #region Agent Commission
        public long AddAgentCommission(AgentCommissionEnt obj)
        {
            return _repo.AddAgentCommission(obj);
        }

        public bool UpdateAgentCommission(AgentCommissionEnt obj)
        {
            return _repo.UpdateAgentCommission(obj);
        }

        public List<AgentCommissionEnt> GetAgentCommissionByFilter(AgentCommissionFilter obj, long pageSize, long pageNumber)
        {
            return _repo.GetAgentCommissionByFilter(obj, pageSize, pageNumber);
        }

        public AgentCommissionEnt GetAgentCommissionById(long? agentCommissionId)
        {
            var filter = new AgentCommissionFilter { AgentCommissionId = agentCommissionId };
            var obj = new AgentCommissionEnt();
            var oList = _repo.GetAgentCommissionByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }
        #endregion

        public bool ConvertPotentialClientToKIVIfLongerThanNDays()
        {
            return _repo.ConvertPotentialClientToKIVIfLongerThanNDays();
        }

        #region Users
        public bool UpdateUserLastLogin(string username)
        {
            return _repo.UpdateUserLastLogin(username);
        }
        #endregion

        #region Report
        public List<LastLoginReport> GetLoginActivityByFilter(LastLoginReportFilter obj)
        {
            return _repo.GetLoginActivityByFilter(obj);
        }

        public List<DuplicateUserReport> GetDuplicateUserByFilter(DuplicateUserFilter obj)
        {
            return _repo.GetDuplicateUserByFilter(obj);
        }
        #endregion


        #region Role Menu 
        public RoleMenuEnt GetMenuByRoleId(int roleId)
        {

            var roleMenu = new RoleMenuEnt { Menus = new List<MainMenuEnt>() };
            var mainMenus =_repo.GetMainMenuByRoleId(roleId);
            if (mainMenus == null)
            {
                return roleMenu;
            }

            foreach (var mainMenu in mainMenus)
            {
                var subMenus = _repo.GetSubMenuByMainMenuId(mainMenu.MainMenuId);
                mainMenu.SubMenus = new List<SubMenuEnt>();
                if (subMenus != null && subMenus.Count > 0)
                {
                    mainMenu.SubMenus = subMenus;
                    roleMenu.Menus.Add(mainMenu);
                }

            }
            return roleMenu;

        }

        public RoleMenuEnt GetAllMenu()
        {

            var roleMenu = new RoleMenuEnt { Menus = new List<MainMenuEnt>() };
            var mainMenus = _repo.GetAllMainMenu();
            if (mainMenus == null)
            {
                return roleMenu;
            }

            foreach (var mainMenu in mainMenus)
            {
                var subMenus = _repo.GetSubMenuByMainMenuId(mainMenu.MainMenuId);
                mainMenu.SubMenus = new List<SubMenuEnt>();
                if (subMenus != null && subMenus.Count > 0)
                {
                    mainMenu.SubMenus = subMenus;
                    roleMenu.Menus.Add(mainMenu);
                }

            }
            return roleMenu;

        }


        public long AddRole(RolesEnt obj)
        {
            #region user id list format
            string subMenuIdList = "";

            if (obj.SubMenuIds != null)
            {
                subMenuIdList = IdListToString(obj.SubMenuIds);
            }
            #endregion

            return _repo.AddRole(obj, subMenuIdList);
        }

        public bool UpdateRole(RolesEnt obj)
        {
            #region user id list format
            string subMenuIdList = "";

            if (obj.SubMenuIds != null)
            {
                subMenuIdList = IdListToString(obj.SubMenuIds);
            }
            #endregion

            return _repo.UpdateRole(obj, subMenuIdList);
        }

        public bool UpdateRoleStatus(long roleId, long statusId, string updatedBy)
        {
            return _repo.UpdateRoleStatus(roleId, statusId, updatedBy);
        }
        public List<RolesEnt> GetRoleByFilter(RoleFilter obj, long pageSize, long pageNumber)
        {
            List<RolesEnt> returnList = new List<RolesEnt>();

            var resultList = _repo.GetRoleByFilter(obj, pageSize, pageNumber);
            return resultList;
        }

        public RolesEnt GetRoleById(long? roleId)
        {
            var filter = new RoleFilter { RoleId = roleId };
            var obj = new RolesEnt();
            var oList = _repo.GetRoleByFilter(filter, ConfigHelper.DefaultPageSize, ConfigHelper.DefaultPageNo);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                    obj.SubMenuIds = _repo.GetSubMenuByRoleId(item.RoleId);
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        #endregion

        //===
        #region Resource
        public bool AddResource(ResourcesEnt obj)
        {
            return _repo.AddResource(obj);
        }

        public bool UpdateResource(ResourcesEnt obj)
        {
            return _repo.UpdateResource(obj);
        }

        public bool UpdateResourceStatus(long ResourceId, long statusId, string updatedBy, long UserId)
        {
            
            return _repo.UpdateResourceStatus(ResourceId, statusId, updatedBy);
        }

     


        public List<ResourcesEnt> GetResourceByFilter(long? ResourceId ,string Name, DateTime? createdDateFrom, DateTime? createdDateTo )
        {
            return _repo.GetResourceByFilter(ResourceId, Name, createdDateFrom, createdDateTo);
        }


        public ResourcesEnt GetResourceById(long? ResourceId)
        {
            var obj = new ResourcesEnt();
            var oList = _repo.GetResourceByFilter(ResourceId, null, null, null);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public ResourcesEnt GetResourceByUsername(ResourceUserRequest request)
        {
            var obj = new ResourcesEnt();
            var oList = _repo.GetResourceByUsername(request);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public ResourcesEnt ResourceValidation (ResourceValidationRequest request)
        {
            var obj = new ResourcesEnt();
            var oList = _repo.ResourceValidation(request);
            if (oList.Count > 0)
            {
                foreach (var item in oList)
                {
                    obj = item;
                }
            }
            else
            {
                obj = null;
            }

            return obj;
        }



        #endregion



        //====
    }
}
