using System;
using System.Collections.Generic;


namespace Caliph.Library.Models
{


    public  class ResourcesEnt : BaseEnt
    {

        public long ResourceId { get; set; }


        public string Name { get; set; }


        public string Url { get; set; }

        //public long? UserId { get; set; }

        public string UserName { get; set; }

        public long? StatusId { get; set; }


    }

    public class ResourceUserRequest : BaseEnt
    {
       
        public string Username { get; set; }
      
    }

    public class ResourceValidationRequest : BaseEnt
    {
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}