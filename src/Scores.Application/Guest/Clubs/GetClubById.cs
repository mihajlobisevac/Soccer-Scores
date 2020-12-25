using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Clubs
{
    public class GetClubById
    {
        private readonly IClubManager clubManager;

        public GetClubById(IClubManager clubManager)
        {
            this.clubManager = clubManager;
        }

        public class Response
        {
            public string Slug { get; set; }
            public string Name { get; set; }
            public string NameCode { get; set; }
            public int YearFounded { get; set; }
            public bool Deactivated { get; set; }
            public int VenueId { get; set; }
        }

        public Response Do(int id) =>
            clubManager.GetClubById(id, x => new Response
            {
                Slug = x.Name.ToLower(),
                Name = x.Name,
                NameCode = x.NameCode,
                YearFounded = x.YearFounded,
                Deactivated = x.Deactivated,
                VenueId = x.VenueId,
            });
    }
}
