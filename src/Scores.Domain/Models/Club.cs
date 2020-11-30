namespace Scores.Domain.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameCode { get; set; }
        public int YearFounded { get; set; }

        public Venue Venue { get; set; }
    }
}
