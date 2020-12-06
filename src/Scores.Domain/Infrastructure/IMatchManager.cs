using Scores.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface IMatchManager
    {
        Task<int> CreateMatch(Match match);
        Task<int> DeleteMatch(int id);
        Task<int> UpdateMatch(Match match);

        TResult GetMatchById<TResult>(int id, Func<Match, TResult> selector);
    }
}
