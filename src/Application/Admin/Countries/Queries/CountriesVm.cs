using SoccerScores.Domain.Entities;
using System.Collections.Generic;

namespace SoccerScores.Application.Admin.Countries.Queries
{
    public class CountriesVm
    {
        public IEnumerable<Country> Countries { get; set; }
    }
}
