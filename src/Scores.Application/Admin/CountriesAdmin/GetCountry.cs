using Scores.Domain.Infrastructure;

namespace Scores.Application.CountriesAdmin
{
    [Service]
    public class GetCountry
    {
        private readonly ICountryManager countryManager;

        public GetCountry(ICountryManager countryManager)
        {
            this.countryManager = countryManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Flag { get; set; }
        }

        public Response Do(int id) =>
            countryManager.GetCountryById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                Flag = x.Flag,
            });
    }
}
