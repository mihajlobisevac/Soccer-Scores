using Scores.Application.Guest.Cities;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Venues
{
    [Service]
    public class GetVenueById
    {
        private readonly IVenueManager venueManager;
        private readonly ICityManager cityManager;
        private readonly ICountryManager countryManager;

        public GetVenueById(IVenueManager venueManager, ICityManager cityManager, ICountryManager countryManager)
        {
            this.venueManager = venueManager;
            this.cityManager = cityManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int YearOpened { get; set; }
            public bool Deactivated { get; set; }
            public GetCityById.Response City { get; set; }
        }

        public Response Do(int id) =>
            venueManager.GetVenueById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                Capacity = x.Capacity,
                YearOpened = x.YearOpened,
                Deactivated = x.Deactivated,
                City = new GetCityById(cityManager, countryManager).Do(x.CityId),
            });
    }
}
