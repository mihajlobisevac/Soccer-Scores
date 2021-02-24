using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

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
        public SeasonViewModel LatestSeason { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Competition, CompetitionViewModel>()
                .ForMember(dest => dest.LatestSeason, opt => 
                    opt.MapFrom(src => src.Seasons.OrderBy(x => x.Start).FirstOrDefault()));
        }
    }

    public class SeasonViewModel : IMapFrom<Season>
    {
        public int Id { get; set; }
    }
}
