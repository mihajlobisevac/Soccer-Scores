using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;

namespace SoccerScores.Application.Matches.Queries.GetMatchesBySeason.Models
{
    public class SeasonWithMatchesVm : IMapFrom<Season>
    {
        public IEnumerable<MatchBySeasonDto> Matches { get; set; }

        public int Id { get; set; }
        public int GameWeek { get; set; }
        public int CompetitionId { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, SeasonWithMatchesVm>()
                .ForMember(dest => dest.Matches, opt => opt.Ignore())
                .ForMember(dest => dest.CompetitionId, opt => opt.MapFrom(src => src.Competition.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Competition.Name));
        }
    }
}
