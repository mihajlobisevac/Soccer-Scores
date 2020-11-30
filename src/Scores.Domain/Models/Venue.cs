namespace Scores.Domain.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int YearOpened { get; set; }

        public City City { get; set; }
    }
}
