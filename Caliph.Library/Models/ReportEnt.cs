using System;

namespace Caliph.Library.Models
{
    #region Last login report
    public class LastLoginReport
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string RoleName { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    public class LastLoginReportFilter
    {
        public string Username { get; set; }
        public long? RoleId { get; set; }
        public DateTime? LastLoginStartDate { get; set; }
        public DateTime? LastLoginEndDate { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
    #endregion

    #region Check duplicate user
    public class DuplicateUserReport
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string IcNo { get; set; }
        public string ContactNo { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class DuplicateUserFilter
    {
        public string Username { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
    #endregion

}
