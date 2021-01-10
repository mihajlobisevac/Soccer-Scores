using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores.Database
{
    public class TournamentManager : ITournamentManager
    {
        private readonly AppDbContext context;

        public TournamentManager(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreateTournament(Tournament tournament)
        {
            context.Tournaments.Add(tournament);

            return context.SaveChangesAsync();
        }

        public Task<int> DeleteTournament(int id)
        {
            var tournament = context.Tournaments.FirstOrDefault(x => x.Id == id);

            context.Tournaments.Remove(tournament);

            return context.SaveChangesAsync();
        }

        public Task<int> UpdateTournament(Tournament tournament)
        {
            context.Tournaments.Update(tournament);

            return context.SaveChangesAsync();
        }

        public TResult GetTournamentById<TResult>(int id, Func<Tournament, TResult> selector)
            => context.Tournaments
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<TResult> GetTournaments<TResult>(Func<Tournament, TResult> selector)
            => context.Tournaments
                .Select(selector)
                .ToList();
    }
}
