using System;

namespace Scores.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int StandingsId { get; set; }
    }
}
