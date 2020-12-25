using Microsoft.EntityFrameworkCore;
using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores.Database
{
    public class MatchManager : IMatchManager
    {
        private readonly AppDbContext context;

        public MatchManager(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreateMatch(Match match)
        {
            context.Matches.Add(match);

            return context.SaveChangesAsync();
        }

        public Task<int> DeleteMatch(int id)
        {
            var match = context.Matches.FirstOrDefault(x => x.Id == id);

            context.Matches.Remove(match);

            return context.SaveChangesAsync();
        }

        public Task<int> UpdateMatch(Match match)
        {
            context.Matches.Update(match);

            return context.SaveChangesAsync();
        }

        public TResult GetMatchById<TResult>(int id, Func<Match, TResult> selector)
            => context.Matches
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<TResult> GetMatchesByDate<TResult>(DateTime date, Func<Match, TResult> selector)
                => context.Matches
                .Where(x => x.KickOff.Date == date.ToUniversalTime().Date)
                .Select(selector)
                .ToList();
    }
}
