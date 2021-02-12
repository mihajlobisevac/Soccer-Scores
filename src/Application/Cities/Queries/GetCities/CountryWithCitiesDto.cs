using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;

namespace SoccerScores.Application.Cities.Queries.GetCities
{
    public class CountryWithCitiesDto : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public IEnumerable<CityViewModel> Cities { get; set; }
    }

    public class CityViewModel : IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
