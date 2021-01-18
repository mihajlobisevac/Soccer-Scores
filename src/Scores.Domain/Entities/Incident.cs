namespace Scores.Domain.Entities
{
    public class Incident
    {
        public int Id { get; set; }

        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public bool IsHome { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }

        public int MatchId { get; set; }
        public int PlayerAId { get; set; }
        public int PlayerBId { get; set; }
    }
}
