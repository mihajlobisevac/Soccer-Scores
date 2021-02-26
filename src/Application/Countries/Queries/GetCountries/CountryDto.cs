using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;

namespace SoccerScores.Application.Countries.Queries.GetCountries
{
    public class CountryDto : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Flag { get; set; }
    }
}
