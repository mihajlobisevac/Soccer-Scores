using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;

namespace SoccerScores.Application.Matches.Queries.GetMatch.Models
{
    public class SeasonVm : IMapFrom<Season>
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, SeasonVm>()
                .ForMember(dest => dest.CompetitionId, opt => opt.MapFrom(src => src.Competition.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Competition.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Competition.Type));
        }
    }
}
