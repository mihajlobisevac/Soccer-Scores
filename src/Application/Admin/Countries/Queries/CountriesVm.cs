using System.Collections.Generic;

namespace SoccerScores.Application.Admin.Countries.Queries
{
    public class CountriesVm
    {
        public IEnumerable<CountryDto> Countries { get; set; }
    }
}
