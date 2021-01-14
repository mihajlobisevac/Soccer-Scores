using Scores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface ITournamentManager
    {
        Task<int> CreateTournament(Tournament tournament);
        Task<int> DeleteTournament(int id);
        Task<int> UpdateTournament(Tournament tournament);

        TResult GetTournamentById<TResult>(int id, Func<Tournament, TResult> selector);
        IEnumerable<TResult> GetTournaments<TResult>(Func<Tournament, TResult> selector);
    }
}
