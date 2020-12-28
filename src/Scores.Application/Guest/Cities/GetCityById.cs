using Scores.Application.Guest.Countries;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Cities
{
    [Service]
    public class GetCityById
    {
        private readonly ICityManager cityManager;
        private readonly ICountryManager countryManager;

        public GetCityById(ICityManager cityManager, ICountryManager countryManager)
        {
            this.cityManager = cityManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Deactivated { get; set; }
            public string Country { get; set; }
        }

        public Response Do(int id) =>
            cityManager.GetCityById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                Deactivated = x.Deactivated,
                Country = new GetCountryById(countryManager).Do(x.CountryId).Name,
            });
    }
}
