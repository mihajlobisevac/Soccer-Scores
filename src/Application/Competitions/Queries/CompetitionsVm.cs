using System.Collections.Generic;

namespace SoccerScores.Application.Competitions.Queries
{
    public class CompetitionsVm
    {
        public IEnumerable<CompetitionDto> Competitions { get; set; }
    }
}
