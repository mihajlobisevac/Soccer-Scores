using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Competitions.Queries.GetCompetitionsByCountry
{
    public class CompetitionDto : IMapFrom<Competition>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
