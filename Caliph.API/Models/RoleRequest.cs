using Caliph.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caliph.API.Models
{
    public class RoleRequest : BaseEnt
    {

        public long RoleId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? StatusId { get; set; }

        public List<long> SubMenuIdList { get; set; }
    }


}