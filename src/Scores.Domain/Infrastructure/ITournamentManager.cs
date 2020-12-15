using Scores.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Scores.Domain.Infrastructure
{
    public interface ITournamentManager
    {
        Task<int> CreateTournament(Tournament tournament);
        Task<int> DeleteTournament(int id);
        Task<int> UpdateTournament(Tournament tournament);

        TResult GetTournamentById<TResult>(int id, Func<Tournament, TResult> selector);
    }
}
