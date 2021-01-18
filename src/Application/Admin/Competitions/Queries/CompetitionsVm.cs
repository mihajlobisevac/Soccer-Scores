using System.Collections.Generic;

namespace SoccerScores.Application.Admin.Competitions.Queries
{
    public class CompetitionsVm
    {
        public IEnumerable<CompetitionDto> Competitions { get; set; }
    }
}
