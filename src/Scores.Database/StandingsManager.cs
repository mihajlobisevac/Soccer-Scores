using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scores.Database
{
    public class StandingsManager : IStandingsManager
    {
        private readonly AppDbContext context;

        public StandingsManager(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreateStandings(Standings standings)
        {
            context.Standings.Add(standings);

            return context.SaveChangesAsync();
        }

        public Task<int> DeleteStandings(int id)
        {
            var standings = context.Standings.FirstOrDefault(x => x.Id == id);

            context.Standings.Remove(standings);

            return context.SaveChangesAsync();
        }

        public Task<int> UpdateStandings(Standings standings)
        {
            context.Standings.Update(standings);

            return context.SaveChangesAsync();
        }

        public TResult GetStandingsById<TResult>(int id, Func<Standings, TResult> selector)
            => context.Standings
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public Task<int> AddClub(int standingsId, int clubId)
        {
            context.ClubStandings.Add(new ClubStandings
            {
                StandingsId = standingsId,
                ClubId = clubId
            });

            return context.SaveChangesAsync();
        }

        public Task<int> RemoveClub(int id)
        {
            var clubStandings = context.ClubStandings.FirstOrDefault(x => x.Id == id);

            context.ClubStandings.Remove(clubStandings);

            return context.SaveChangesAsync();
        }
    }
}
