using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;

namespace SoccerScores.Application.Seasons.Queries.GetSeason
{
    public class SeasonDto : IMapFrom<Season>
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Competition { get; set; }
        public string Country { get; set; }
        public string Flag { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, SeasonDto>()
                .ForMember(dest => dest.Competition, opt => opt.MapFrom(src => src.Competition.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Competition.Country.Name))
                .ForMember(dest => dest.Flag, opt => opt.MapFrom(src => src.Competition.Country.Flag));
        }
    }
}
