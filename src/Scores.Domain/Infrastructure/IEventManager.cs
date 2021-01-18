using Scores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface IEventManager
    {
        Task<int> CreateEvent(Incident evnt);
        Task<int> DeleteEvent(int id);
        Task<int> UpdateEvent(Incident evnt);

        TResult GetEventById<TResult>(int id, Func<Incident, TResult> selector);
        IEnumerable<TResult> GetEventsByMatchId<TResult>(int id, Func<Incident, TResult> selector);
    }
}
