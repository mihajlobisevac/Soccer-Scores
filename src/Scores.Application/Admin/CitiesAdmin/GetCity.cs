using Scores.Domain.Infrastructure;

namespace Scores.Application.CitiesAdmin
{
    [Service]
    public class GetCity
    {
        private readonly ICityManager cityManager;

        public GetCity(ICityManager cityManager)
        {
            this.cityManager = cityManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int CountryId { get; set; }
        }

        public Response Do(int id) =>
            cityManager.GetCityById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
            });
    }
}
