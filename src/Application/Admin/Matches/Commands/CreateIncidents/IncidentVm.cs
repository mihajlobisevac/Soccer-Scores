namespace SoccerScores.Application.Admin.Matches.Commands.CreateIncidents
{
    public class IncidentVm
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public bool IsHome { get; set; }
        public int MatchId { get; set; }
        public int? PrimaryPlayerId { get; set; }
        public int? SecondaryPlayerId { get; set; }
    }
}
