﻿using AutoMapper;
using SoccerScores.Application.Admin.Matches.Queries.GetMatch.ViewModels;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SoccerScores.Application.Admin.Matches.Queries.GetMatch
{
    public class MatchDto : IMapFrom<Match>
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }

        public ClubViewModel HomeTeam { get; set; }
        public ClubViewModel AwayTeam { get; set; }
        public SeasonViewModel Season { get; set; }

        public ICollection<IncidentViewModel> Incidents { get; set; }
        public ICollection<MatchPlayerViewModel> Players { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Match, MatchDto>()
                .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam))
                .ForMember(dest => dest.AwayTeam, opt => opt.MapFrom(src => src.AwayTeam))
                .ForMember(dest => dest.Incidents, opt => opt.MapFrom(src => src.Incidents))
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players));
        }
    }
}
