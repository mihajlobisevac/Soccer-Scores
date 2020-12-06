using Scores.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface IEventManager
    {
        Task<int> CreateEvent(Event evnt);
        Task<int> DeleteEvent(int id);
        Task<int> UpdateEvent(Event evnt);

        TResult GetEventById<TResult>(int id, Func<Event, TResult> selector);
    }
}
