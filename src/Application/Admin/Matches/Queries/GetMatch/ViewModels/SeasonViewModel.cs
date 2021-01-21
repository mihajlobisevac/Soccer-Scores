using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;

namespace SoccerScores.Application.Admin.Matches.Queries.GetMatch.ViewModels
{
    public class SeasonViewModel : IMapFrom<Season>
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, SeasonViewModel>()
                .ForMember(dest => dest.CompetitionId, opt => opt.MapFrom(src => src.Competition.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Competition.Name));
        }
    }
}
