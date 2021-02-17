using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Cities.Queries.GetCitiesByCountry
{
    public class CityDto : IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
