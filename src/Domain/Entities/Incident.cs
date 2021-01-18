using Domain.Enums;

namespace SoccerScores.Domain.Entities
{
    public class Incident
    {
        public int Id { get; set; }

        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public IncidentType Type { get; set; }
        public IncidentClass Class { get; set; }
        public bool IsHome { get; set; }

        public Match Match { get; set; }
        public Player PrimaryPlayer { get; set; }
        public Player SecondaryPlayer { get; set; }
    }
}
