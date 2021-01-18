using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scores.Application.ClubsAdmin
{
    [Service]
    public class UpdateClub
    {
        private readonly IClubManager clubManager;

        public UpdateClub(IClubManager clubManager)
        {
            this.clubManager = clubManager;
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameCode { get; set; }
            public int YearFounded { get; set; }
            public string Logo { get; set; }
            public bool Deactivated { get; set; }
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
            var club = clubManager.GetClubById(request.Id, x => x);

            club.Name = request.Name;
            club.YearFounded = request.YearFounded;
            club.Logo = request.Logo;
            club.VenueId = request.VenueId;

            await clubManager.UpdateClub(club);

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
