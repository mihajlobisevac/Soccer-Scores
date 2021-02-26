using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;

namespace SoccerScores.Application.Matches.Queries.GetMatchesByDate
{
    public class MatchByDateDto : IMapFrom<Match>
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }
        public int GameWeek { get; set; }

        public ClubViewModel HomeTeam { get; set; }
        public ClubViewModel AwayTeam { get; set; }
        public SeasonViewModel Season { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Match, MatchByDateDto>()
                .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam))
                .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam));
        }
    }
}
