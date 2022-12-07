using System;
using System.Collections.Generic;

namespace Caliph.Library.Models
{
    public class RolesEnt : BaseEnt
    {
        public long RoleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? StatusId { get; set; }
        public string StatusDesc { get; set; }
        public List<long> SubMenuIds { get; set; }
    }


    public class RoleMenuEnt {
        public List<MainMenuEnt> Menus { get; set; }

    }
    public class MainMenuEnt {
        public long MainMenuId { get; set; }
        public string Name { get; set; }
        public List<SubMenuEnt> SubMenus { get; set; }

    }

    public class SubMenuEnt {
        public long SubMenuId { get; set; }
        public string Name { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
    }


    public class RoleFilter
    {
        public long? RoleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? StatusId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
