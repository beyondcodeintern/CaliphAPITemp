namespace Caliph.Library.Models
{
    public class ActivityPointsEnt : BaseEnt
    {
        public long ActivityPointId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
    }
}
