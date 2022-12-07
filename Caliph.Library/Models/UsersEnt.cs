using System;
using System.Collections.Generic;

namespace Caliph.Library.Models
{
    public class UsersEnt : BaseEnt
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string PW { get; set; }
        public string Fullname { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public long RoleId { get; set; }
        public string IcNo { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public long UplineUserId { get; set; }
        public string UplineUsername { get; set; }
        public string UplineDisplayName { get; set; }
        public List<UsersMenuEnt> MenuList { get; set; } = new List<UsersMenuEnt>();
    }

    public class UsersMenuEnt
    {
        public long MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public long SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
    }

    public class AgentEnt
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Fullname { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public long RoleId { get; set; }
        public long UplineUserId { get; set; }
        public string UplineUsername { get; set; }
        public string UplineDisplayName { get; set; }
    }

    public class SystemUserRequest : BaseEnt
    {
        public long? UserId { get; set; }
        public string Username { get; set; }
        public string PW { get; set; }
        public long? RoleId { get; set; }
        public long? StatusId { get; set; }
        public long? UplineUserId { get; set; }
        public string UplineUsername { get; set; }
        public string DisplayName { get; set; }
        public string Fullname { get; set; }
        public string IcNo { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }

    public class SystemUserFilter : BaseEnt
    {
        public long? UserId { get; set; }
        public string Username { get; set; }
        public string IcNo { get; set; }
        public long? RoleId { get; set; }
        public long? StatusId { get; set; }
        public long? UplineUserId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }

    public class ConvertOne2OneAgentRequest
    {
        public string Username { get; set; }
        public string NewUsername { get; set; }
        public long RoleId { get; set; }
    }


    public class UsersStaffEnt : BaseEnt
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public long RoleId { get; set; }
        public string IcNo { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
    }
}
