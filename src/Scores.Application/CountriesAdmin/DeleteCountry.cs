using System.Threading.Tasks;
using Scores.Domain.Infrastructure;

namespace Scores.Application.CountriesAdmin
{
    [Service]
    public class DeleteCountry
    {
        private readonly ICountryManager countryManager;

        public DeleteCountry(ICountryManager countryManager)
        {
            this.countryManager = countryManager;
        }
        
        public Task<int> Do(int id) => countryManager.DeleteCountry(id);
    }
}
