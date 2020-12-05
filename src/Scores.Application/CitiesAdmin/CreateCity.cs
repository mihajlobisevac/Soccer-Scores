using System;
using System.Threading.Tasks;
using Scores.Domain.Infrastructure;
using Scores.Domain.Models;

namespace Scores.Application.CitiesAdmin
{
    [Service]
    public class CreateCity
    {
        private readonly ICityManager cityManager;

        public CreateCity(ICityManager cityManager)
        {
            this.cityManager = cityManager;
        }

        public class Request
        {
            public string Name { get; set; }
            public int CountryId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CountryId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var city = new City
            {
                Name = request.Name,
                CountryId = request.CountryId
            };

            var result = await cityManager.CreateCity(city);
            
            if (result <= 0)
                throw new Exception("Failed to create city");

            return new Response
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId
            };
        }
    }
}
