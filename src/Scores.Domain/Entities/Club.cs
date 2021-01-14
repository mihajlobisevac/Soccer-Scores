namespace Scores.Domain.Entities
{
    public class Club
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameCode { get; set; }
        public string Logo { get; set; }
        public int YearFounded { get; set; }

        public int VenueId { get; set; }
        public int TournamentId { get; set; }
    }
}
