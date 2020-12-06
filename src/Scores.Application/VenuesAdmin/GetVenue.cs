using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.VenuesAdmin
{
    [Service]
    public class GetVenue
    {
        private readonly IVenueManager venueManager;

        public GetVenue(IVenueManager venueManager)
        {
            this.venueManager = venueManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int YearOpened { get; set; }
            public int CityId { get; set; }
        }

        public Response Do(int id) =>
            venueManager.GetVenueById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                Capacity = x.Capacity,
                YearOpened = x.YearOpened,
                CityId = x.CityId,
            });
    }
}
