using Scores.Domain.Infrastructure;
using Scores.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scores.Database
{
    public class VenueManager : IVenueManager
    {
        private readonly AppDbContext context;

        public VenueManager(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreateVenue(Venue venue)
        {
            context.Venues.Add(venue);

            return context.SaveChangesAsync();
        }

        public Task<int> DeleteVenue(int id)
        {
            var venue = context.Venues.FirstOrDefault(x => x.Id == id);

            context.Venues.Remove(venue);

            return context.SaveChangesAsync();
        }

        public Task<int> UpdateVenue(Venue venue)
        {
            context.Venues.Update(venue);

            return context.SaveChangesAsync();
        }

        public TResult GetVenueById<TResult>(int id, Func<Venue, TResult> selector)
            => context.Venues
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
    }
}
