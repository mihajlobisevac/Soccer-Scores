using Domain.Enums;
using System;
using System.Collections.Generic;

namespace SoccerScores.Domain.Entities
{
    public class Competition
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public CompetitionType Type { get; set; }

        public Country Country { get; set; }
        public ICollection<Season> Seasons { get; set; }

        public object FirstOrDefaultAsync(int seasonId)
        {
            throw new NotImplementedException();
        }
    }
}
