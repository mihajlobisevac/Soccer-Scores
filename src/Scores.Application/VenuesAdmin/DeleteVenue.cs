using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.VenuesAdmin
{
    [Service]
    public class DeleteVenue
    {
        private readonly IVenueManager venueManager;

        public DeleteVenue(IVenueManager venueManager)
        {
            this.venueManager = venueManager;
        }

        public Task<int> Do(int id) => venueManager.DeleteVenue(id);
    }
}
