using AutoMapper;
using Domain.Enums;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;

namespace SoccerScores.Application.Admin.Clubs.Queries
{
    public class PlayerDto : IMapFrom<Player>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Position Position { get; set; }
        public Foot Foot { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Nationality { get; set; }
        public string PlaceOfBirth { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Player, PlayerDto>()
                .ForMember(
                    dest => dest.Nationality,
                    opt => opt.MapFrom(src => src.Nationality.Name)
                )
                .ForMember(
                    dest => dest.PlaceOfBirth,
                    opt => opt.MapFrom(src => src.PlaceOfBirth.Name)
                );
        }
    }
}
