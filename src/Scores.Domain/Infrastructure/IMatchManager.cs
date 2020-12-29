using Scores.Domain.Models;
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
    }
}
