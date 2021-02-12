using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Cities.Queries.GetCity
{
    public class CityDto : IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<City, CityDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name));
        }
    }
}
