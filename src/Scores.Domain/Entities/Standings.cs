using System;

namespace Scores.Domain.Entities
{
    public class Standings
    {
        public int Id { get; set; }

        public DateTime SeasonStart { get; set; }
        public DateTime SeasonEnd { get; set; }

        public int TournamentId { get; set; }
    }
}
