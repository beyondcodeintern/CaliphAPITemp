namespace Caliph.Library.Models
{


    public partial class ResourcesEnt : BaseEnt
    {

        public long ResourceId { get; set; }


        public string Name { get; set; }


        public string Url { get; set; }

        public long? UserId { get; set; }

        public string UserName { get; set; }

        public long? StatusId { get; set; }


    }
}