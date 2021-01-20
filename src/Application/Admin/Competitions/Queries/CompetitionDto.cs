﻿using AutoMapper;
using Domain.Enums;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Admin.Competitions.Queries
{
    public class CompetitionDto : IMapFrom<Competition>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompetitionType Type { get; set; }
        public string Country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Competition, CompetitionDto>()
                .ForMember(
                    dest => dest.Country,
                    opt => opt.MapFrom(src => src.Country.Name)
                );
        }
    }
}