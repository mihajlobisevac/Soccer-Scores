using System.Collections.Generic;

namespace SoccerScores.Application.Admin.Seasons.Queries
{
    public class SeasonsVm
    {
        public IEnumerable<SeasonDto> Seasons { get; set; }
    }
}
