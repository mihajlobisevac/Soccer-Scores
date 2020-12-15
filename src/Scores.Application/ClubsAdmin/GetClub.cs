using Scores.Domain.Infrastructure;

namespace Scores.Application.ClubsAdmin
{
    [Service]
    public class GetClub
    {
        private readonly IClubManager clubManager;

        public GetClub(IClubManager clubManager)
        {
            this.clubManager = clubManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameCode { get; set; }
            public int YearFounded { get; set; }
            public bool Deactivated { get; set; }
            public int VenueId { get; set; }
        }

        public Response Do(int id) =>
            clubManager.GetClubById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                NameCode = x.NameCode,
                YearFounded = x.YearFounded,
                Deactivated = x.Deactivated,
                VenueId = x.VenueId,
            });
    }
}
