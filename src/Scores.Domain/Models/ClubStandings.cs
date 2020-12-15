using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Domain.Models
{
    public class ClubStandings
    {
        public int Id { get; set; }

        public int ClubId { get; set; }
        public int StandingsId { get; set; }
    }
}
