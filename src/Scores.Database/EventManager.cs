using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores.Database
{
    public class EventManager : IEventManager
    {
        private readonly AppDbContext context;

        public EventManager(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreateEvent(Event evnt)
        {
            context.Events.Add(evnt);

            return context.SaveChangesAsync();
        }

        public Task<int> DeleteEvent(int id)
        {
            var evnt = context.Events.FirstOrDefault(x => x.Id == id);

            context.Events.Remove(evnt);

            return context.SaveChangesAsync();
        }

        public Task<int> UpdateEvent(Event evnt)
        {
            context.Events.Update(evnt);

            return context.SaveChangesAsync();
        }

        public TResult GetEventById<TResult>(int id, Func<Event, TResult> selector)
            => context.Events
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<TResult> GetEventsByMatchId<TResult>(int id, Func<Event, TResult> selector)
            => context.Events
                .Where(x => x.MatchId == id)
                .Select(selector)
                .ToList();
    }
}
