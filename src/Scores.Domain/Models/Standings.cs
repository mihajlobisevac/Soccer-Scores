using System;

namespace Scores.Domain.Models
{
    public class Standings
    {
        public int Id { get; set; }
        public int TeamCount { get; set; }
        public DateTime SeasonStart { get; set; }
        public DateTime SeasonEnd { get; set; }

        public bool Deactivated { get; set; }

        public int TournamentId { get; set; }
    }
}
