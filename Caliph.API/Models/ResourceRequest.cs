using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class ResourceRequest : BaseEnt
    {
        public long ResourceId { get; set; }


        public string Name { get; set; }


        public string Url { get; set; }

        public long? UserId { get; set; }

        public string UserName { get; set; }

        public long? StatusId { get; set; }
    }

    public class ResourceFilterRequest
    {

        public long ResourceId { get; set; }


        public string Name { get; set; }


        public string Url { get; set; }

        //public long? UserId { get; set; }

        public string UserName { get; set; }

      
        public long? StatusId { get; set; }
      
      
        public string CreatedBy { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}