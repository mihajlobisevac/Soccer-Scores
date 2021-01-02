using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scores.Database
{
    public class PlayerManager : IPlayerManager
    {
        private readonly AppDbContext context;

        public PlayerManager(AppDbContext context)
        {
            this.context = context;
        }

        public Task<int> CreatePlayer(Player player)
        {
            context.Players.Add(player);

            return context.SaveChangesAsync();
        }

        public Task<int> DeletePlayer(int id)
        {
            var player = context.Players.FirstOrDefault(x => x.Id == id);

            context.Players.Remove(player);

            return context.SaveChangesAsync();
        }

        public Task<int> UpdatePlayer(Player player)
        {
            context.Players.Update(player);

            return context.SaveChangesAsync();
        }

        public TResult GetPlayerById<TResult>(int id, Func<Player, TResult> selector)
            => context.Players
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public IEnumerable<TResult> GetPlayersByClubId<TResult>(int id, Func<Player, TResult> selector)
            => context.Players
                .Where(x => x.ClubId == id)
                .Select(selector)
                .ToList();

        public IEnumerable<TResult> GetPlayersByMatchId<TResult>(int id, Func<MatchPlayer, TResult> selector)
            => context.MatchPlayers
                .Where(x => x.MatchId == id)
                .Select(selector)
                .ToList();
    }
}
