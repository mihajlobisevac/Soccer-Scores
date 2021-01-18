using Scores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface IStandingsManager
    {
        Task<int> CreateStandings(Standings standings);
        Task<int> DeleteStandings(int id);
        Task<int> UpdateStandings(Standings standings);

        TResult GetStandingsById<TResult>(int id, Func<Standings, TResult> selector);
        TResult GetStandingsByTournamentId<TResult>(int id, Func<Standings, TResult> selector);

        Task<int> AddClub(ClubStandings clubStandings);
        Task<int> RemoveClub(int id);
    }
}
