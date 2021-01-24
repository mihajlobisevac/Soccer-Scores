using System.Collections.Generic;

namespace SoccerScores.Application.Seasons.Queries
{
    public class SeasonsVm
    {
        public IEnumerable<SeasonDto> Seasons { get; set; }
    }
}
