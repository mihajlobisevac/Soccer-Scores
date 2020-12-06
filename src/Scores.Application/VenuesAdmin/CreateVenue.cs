using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Scores.Application.VenuesAdmin
{
    [Service]
    public class CreateVenue
    {
        private readonly IVenueManager venueManager;

        public CreateVenue(IVenueManager venueManager)
        {
            this.venueManager = venueManager;
        }

        public class Request
        {
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
            var venue = new Venue
            {
                Name = request.Name,
                Capacity = request.Capacity,
                YearOpened = request.YearOpened,
                CityId = request.CityId,
            };

            var result = await venueManager.CreateVenue(venue);

            if (result <= 0)
                throw new Exception("Failed to create venue");

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
