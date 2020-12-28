using Scores.Application.Guest.Venues;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Clubs
{
    [Service]
    public class GetClubById
    {
        private readonly IClubManager clubManager;
        private readonly IVenueManager venueManager;
        private readonly ICityManager cityManager;
        private readonly ICountryManager countryManager;

        public GetClubById(IClubManager clubManager, IVenueManager venueManager, 
            ICityManager cityManager, ICountryManager countryManager)
        {
            this.clubManager = clubManager;
            this.venueManager = venueManager;
            this.cityManager = cityManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Slug { get; set; }
            public string Name { get; set; }
            public string NameCode { get; set; }
            public int YearFounded { get; set; }
            public string Logo { get; set; }
            public bool Deactivated { get; set; }
            public GetVenueById.Response Venue { get; set; }
        }

        public Response Do(int id) =>
            clubManager.GetClubById(id, x => new Response
            {
                Id = x.Id,
                Slug = x.Name.Replace(" ", "").Replace(".", ""),
                Name = x.Name,
                NameCode = x.NameCode,
                YearFounded = x.YearFounded,
                Logo = x.Logo,
                Deactivated = x.Deactivated,
                Venue = new GetVenueById(venueManager, cityManager, countryManager).Do(x.VenueId),
            });
    }
}
