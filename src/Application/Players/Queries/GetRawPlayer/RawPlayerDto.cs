using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;

namespace SoccerScores.Application.Players.Queries.GetRawPlayer
{
    public class RawPlayerDto : IMapFrom<ClubPlayer>, IMapFrom<Player>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public string Foot { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public CountryViewModel Nationality { get; set; }
        public CityViewModel PlaceOfBirth { get; set; }
        public ClubViewModel Club { get; set; }
        public int ShirtNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClubPlayer, RawPlayerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Player.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Player.DateOfBirth))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Player.Position.ToString()))
                .ForMember(dest => dest.Foot, opt => opt.MapFrom(src => src.Player.Foot.ToString()))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Player.Height))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Player.Weight))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Player.Nationality))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.Player.PlaceOfBirth))
                .ForMember(dest => dest.Club, opt => opt.MapFrom(src => src.Club));

            profile.CreateMap<Player, RawPlayerDto>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.ToString()))
                .ForMember(dest => dest.Foot, opt => opt.MapFrom(src => src.Foot.ToString()))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.PlaceOfBirth, opt => opt.MapFrom(src => src.PlaceOfBirth));
        }
    }

    public class ClubViewModel : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Crest { get; set; }
        public CountryViewModel Country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Club, ClubViewModel>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country));
        }
    }

    public class CountryViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
    }

    public class CityViewModel : IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryViewModel Country { get; set; }
    }

    public class CompetitionViewModel : IMapFrom<Season>
    {
        public int Id { get; set; }
        public int LatestSeasonId { get; set; }
        public string Name { get; set; }
        public CountryViewModel Country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, CompetitionViewModel>()
                .ForMember(dest => dest.LatestSeasonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Competition.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Competition.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Competition.Country));
        }
    }
}
