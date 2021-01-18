namespace Scores.Domain.Entities
{
    public class Venue
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public int YearOpened { get; set; }

        public int CityId { get; set; }
    }
}
