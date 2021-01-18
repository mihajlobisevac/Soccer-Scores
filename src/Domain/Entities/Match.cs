using System;
using System.Collections.Generic;

namespace SoccerScores.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }

        public Club HomeTeam { get; set; }
        public Club AwayTeam { get; set; }
        public Season Season { get; set; }
        public ICollection<Incident> Incidents { get; set; }
        public ICollection<MatchPlayer> Players { get; set; }
    }
}
