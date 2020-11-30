namespace Scores.Domain.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public bool IsHome { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }

        public Match Match { get; set; }
        public Player PlayerA { get; set; }
        public Player PlayerB { get; set; }
    }
}
