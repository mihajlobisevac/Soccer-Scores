using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;

namespace SoccerScores.Application.Admin.Clubs.Queries.GetClubWithPlayersQuery
{
    public class ClubWithPlayersDto : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Crest { get; set; }
        public string Venue { get; set; }
        public int YearFounded { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public IEnumerable<ClubPlayerDto> Players { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Club, ClubWithPlayersDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country.Name));
        }
    }
}
