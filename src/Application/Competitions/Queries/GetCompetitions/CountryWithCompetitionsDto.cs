using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;

namespace SoccerScores.Application.Competitions.Queries.GetCompetitions
{
    public class CountryWithCompetitionsDto : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Flag { get; set; }

        public ICollection<CompetitionViewModel> Competitions { get; set; }
    }

    public class CompetitionViewModel : IMapFrom<Competition>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
