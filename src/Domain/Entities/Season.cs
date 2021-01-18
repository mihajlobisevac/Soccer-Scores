using System;

namespace SoccerScores.Domain.Entities
{
    public class Season
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Competition Competition { get; set; }
    }
}
