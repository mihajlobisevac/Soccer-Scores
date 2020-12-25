using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.CountriesAdmin
{
    [Service]
    public class UpdateCountry
    {
        private readonly ICountryManager countryManager;

        public UpdateCountry(ICountryManager countryManager)
        {
            this.countryManager = countryManager;
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameCode { get; set; }
            public string Flag { get; set; }
            public bool Deactivated { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameCode { get; set; }
            public string Flag { get; set; }
            public bool Deactivated { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var country = countryManager.GetCountryById(request.Id, x => x);

            country.Name = request.Name;
            country.NameCode = request.NameCode;
            country.Flag = request.Flag;
            country.Deactivated = request.Deactivated;

            await countryManager.UpdateCountry(country);

            return new Response
            {
                Id = country.Id,
                Name = country.Name,
                NameCode = country.NameCode,
                Flag = country.Flag,
                Deactivated = country.Deactivated,
            };
        }
    }
}
