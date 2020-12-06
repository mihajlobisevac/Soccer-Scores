using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scores.Database
{
    public class ClubManager : IClubManager
    {
        private readonly AppDbContext context;

        public ClubManager(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreateClub(Club club)
        {
            context.Clubs.Add(club);

            return context.SaveChangesAsync();
        }

        public Task<int> DeleteClub(int id)
        {
            var club = context.Clubs.FirstOrDefault(x => x.Id == id);

            context.Clubs.Remove(club);

            return context.SaveChangesAsync();
        }

        public Task<int> UpdateClub(Club club)
        {
            context.Clubs.Update(club);

            return context.SaveChangesAsync();
        }

        public TResult GetClubById<TResult>(int id, Func<Club, TResult> selector)
            => context.Clubs
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
    }
}
