using System;

namespace Scores.Domain.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime KickOff { get; set; }

        public bool Deactivated { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
    }
}
