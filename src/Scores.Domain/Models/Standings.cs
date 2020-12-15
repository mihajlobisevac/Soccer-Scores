using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Domain.Models
{
    public class Standings
    {
        public int Id { get; set; }
        public int TeamCount { get; set; }

        public bool Deactivated { get; set; }

        public int TournamentId { get; set; }
    }
}
