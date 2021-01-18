using Scores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface IMatchManager
    {
        Task<int> CreateMatch(Match match);
        Task<int> DeleteMatch(int id);
        Task<int> UpdateMatch(Match match);

        TResult GetMatchById<TResult>(int id, Func<Match, TResult> selector);
        IEnumerable<TResult> GetMatchesByDate<TResult>(DateTime date, Func<Match, TResult> selector);
        IEnumerable<TResult> GetMatchesByClubId<TResult>(int id, Func<Match, TResult> selector);
        IEnumerable<TResult> GetMatchesByPlayerId<TResult>(int id, Func<MatchPlayer, TResult> selector);
        IEnumerable<TResult> GetMatchesByStandingsId<TResult>(int id, Func<Match, TResult> selector);
    }
}
