﻿using AutoMapper;
using Domain.Enums;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;
using System.Linq;

namespace SoccerScores.Application.Matches.Queries.GetMatchesByClub
{
    public class MatchByClubDto : IMapFrom<Match>
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }
        public int GameWeek { get; set; }

        public ClubViewModel HomeTeam { get; set; }
        public ClubViewModel AwayTeam { get; set; }
        public SeasonViewModel Season { get; set; }

        public ResultViewModel Result { get; set; }
        public bool IsHome { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Match, MatchByClubDto>()
                .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam))
                .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Incidents.FirstOrDefault(x => x.Class == IncidentClass.FT)));
        }
    }
}
