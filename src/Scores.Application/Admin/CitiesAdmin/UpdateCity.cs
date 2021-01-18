using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.CitiesAdmin
{
    [Service]
    public class UpdateCity
    {
        private readonly ICityManager cityManager;

        public UpdateCity(ICityManager cityManager)
        {
            this.cityManager = cityManager;
        }

        public class Request
        {
            public int Id { get; set; }
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
            var city = cityManager.GetCityById(request.Id, x => x);

            city.Name = request.Name;
            city.CountryId = request.CountryId;

            await cityManager.UpdateCity(city);

            return new Response
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
            };
        }
    }
}
