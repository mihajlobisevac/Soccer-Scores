using Scores.Domain.Infrastructure;
using Scores.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Scores.Application.ClubsAdmin
{
    [Service]
    public class CreateClub
    {
        private readonly IClubManager clubManager;

        public CreateClub(IClubManager clubManager)
        {
            this.clubManager = clubManager;
        }

        public class Request
        {
            public string Name { get; set; }
            public int YearFounded { get; set; }
            public string Logo { get; set; }
            public int VenueId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int YearFounded { get; set; }
            public string Logo { get; set; }
            public int VenueId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var club = new Club
            {
                Name = request.Name,
                YearFounded = request.YearFounded,
                Logo = request.Logo,
                VenueId = request.VenueId,
            };

            var result = await clubManager.CreateClub(club);

            if (result <= 0)
                throw new Exception("Failed to create club");

            return new Response
            {
                Id = club.Id,
                Name = club.Name,
                YearFounded = club.YearFounded,
                Logo = club.Logo,
                VenueId = club.VenueId,
            };
        }
    }
}
