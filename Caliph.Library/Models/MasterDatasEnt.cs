namespace Caliph.Library.Models
{
    public class MasterDatasEnt : BaseEnt
    {
        public long MasterDataId { get; set; }
        public long MasterId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
    }
}
