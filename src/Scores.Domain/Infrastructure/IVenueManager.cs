using Scores.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface IVenueManager
    {
        Task<int> CreateVenue(Venue venue);
        Task<int> DeleteVenue(int id);
        Task<int> UpdateVenue(Venue venue);

        TResult GetVenueById<TResult>(int id, Func<Venue, TResult> selector);
    }
}
