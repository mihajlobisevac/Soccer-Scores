using System;
using System.Threading.Tasks;
using Scores.Domain.Infrastructure;
using Scores.Domain.Models;

namespace Scores.Application.CountriesAdmin
{
    [Service]
    public class CreateCountry
    {
        private readonly ICountryManager countryManager;

        public CreateCountry(ICountryManager countryManager)
        {
            this.countryManager = countryManager;
        }

        public class Request
        {
            public string Name { get; set; }
            public string NameCode { get; set; }
            public string Flag { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameCode { get; set; }
            public string Flag { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var country = new Country
            {
                Name = request.Name,
                NameCode = request.NameCode,
                Flag = request.Flag
            };

            var result = await countryManager.CreateCountry(country);

            if (result <= 0)
                throw new Exception("Failed to create country");

            return new Response
            {
                Id = country.Id,
                Name = request.Name,
                NameCode = request.NameCode,
                Flag = request.Flag
            };
        }
    }
}
