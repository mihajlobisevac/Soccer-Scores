using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scores.Application.VenuesAdmin
{
    [Service]
    public class UpdateVenue
    {
        private readonly IVenueManager venueManager;

        public UpdateVenue(IVenueManager venueManager)
        {
            this.venueManager = venueManager;
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int YearOpened { get; set; }
            public int CityId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int YearOpened { get; set; }
            public int CityId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var venue = venueManager.GetVenueById(request.Id, x => x);

            venue.Name = request.Name;
            venue.Capacity = request.Capacity;
            venue.YearOpened = request.YearOpened;
            venue.CityId = request.CityId;

            await venueManager.UpdateVenue(venue);

            return new Response
            {
                Id = venue.Id,
                Name = venue.Name,
                Capacity = venue.Capacity,
                YearOpened = venue.YearOpened,
                CityId = venue.CityId,
            };
        }
    }
}
