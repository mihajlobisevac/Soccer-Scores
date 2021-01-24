using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SoccerScores.Application.Countries.Queries
{
    public class CountryDto : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Flag { get; set; }

        public ICollection<string> Cities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryDto>()
                .ForMember(
                    dest => dest.Cities, 
                    opt => opt.MapFrom(src => src.Cities.Select(c => c.Name))
                );
        }
    }
}
