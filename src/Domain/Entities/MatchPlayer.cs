namespace SoccerScores.Domain.Entities
{
    public class MatchPlayer
    {
        public int Id { get; set; }

        public bool IsHome { get; set; }
        public bool IsSubstitute { get; set; }
        public int ShirtNumber { get; set; }

        public Match Match { get; set; }
        public Player Player { get; set; }
    }
}
