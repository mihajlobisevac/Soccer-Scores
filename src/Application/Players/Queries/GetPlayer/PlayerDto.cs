using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;

namespace SoccerScores.Application.Players.Queries.GetPlayer
{
    public class PlayerDto : IMapFrom<ClubPlayer>, IMapFrom<Player>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public string Foot { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Nationality { get; set; }
        public string PlaceOfBirth { get; set; }
        public int ShirtNumber { get; set; }

        public ClubViewModel Club { get; set; }

        public class ClubViewModel : IMapFrom<Club>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Crest { get; set; }
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClubPlayer, PlayerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Player.DateOfBirth))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Player.Position.ToString()))
                .ForMember(dest => dest.Foot, opt => opt.MapFrom(src => src.Player.Foot.ToString()))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Player.Height))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Player.Weight))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Player.Nationality.Name))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.Player.PlaceOfBirth.Name))
                .ForMember(dest => dest.Club, opt => opt.MapFrom(src => src.Club));

            profile.CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.ToString()))
                .ForMember(dest => dest.Foot, opt => opt.MapFrom(src => src.Foot.ToString()))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality.Name))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.PlaceOfBirth.Name));
        }
    }
}
