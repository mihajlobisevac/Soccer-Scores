using AutoMapper;
using Domain.Enums;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerScores.Application.Fixtures.Queries
{
    public class CompetitionVm : IMapFrom<Season>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Flag { get; set; }
        public string Type { get; set; }

        public List<MatchViewModel> Matches { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, CompetitionVm>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Competition.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Competition.Country.Name))
                .ForMember(dest => dest.Flag, opt => opt.MapFrom(src => src.Competition.Country.Flag))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Competition.Type));
        }
    }

    public class MatchViewModel : IMapFrom<Match>
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }
        public int GameWeek { get; set; }

        public ClubViewModel HomeTeam { get; set; }
        public ClubViewModel AwayTeam { get; set; }

        public ResultViewModel Result { get; set; }
        public IEnumerable<PlayerViewModel> Players { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Match, MatchViewModel>()
                .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam))
                .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Incidents.FirstOrDefault(x => x.Class == IncidentClass.FT)));
        }
    }

    public class ClubViewModel : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Crest { get; set; }
    }

    public class PlayerViewModel : IMapFrom<MatchPlayer>
    {
        public int Id { get; set; }
        public bool IsHome { get; set; }
        public bool IsSubstitute { get; set; }
        public string ShirtNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MatchPlayer, PlayerViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName));
        }
    }

    public class ResultViewModel : IMapFrom<Incident>
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
