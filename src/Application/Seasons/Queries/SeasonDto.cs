using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;

namespace SoccerScores.Application.Seasons.Queries
{
    public class SeasonDto : IMapFrom<Season>
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Competition { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, SeasonDto>()
                .ForMember(
                    dest => dest.Competition,
                    opt => opt.MapFrom(src => src.Competition.Name)
                );
        }
    }
}
