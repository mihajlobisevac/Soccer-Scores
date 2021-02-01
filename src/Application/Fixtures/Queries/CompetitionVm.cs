using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SoccerScores.Application.Fixtures.Queries
{
    public class CompetitionVm : IMapFrom<Season>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public List<MatchViewModel> Matches { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, CompetitionVm>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Competition.Name))
                .ForMember(dest => dest.Flag, opt => opt.MapFrom(src => src.Competition.Country.Flag));
        }
    }

    public class MatchViewModel : IMapFrom<Match>
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }
        public int GameWeek { get; set; }

        public ClubViewModel HomeTeam { get; set; }
        public ClubViewModel AwayTeam { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Match, MatchViewModel>()
                .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam))
                .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam));
        }
    }

    public class ClubViewModel : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Crest { get; set; }
    }
}
