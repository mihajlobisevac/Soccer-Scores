using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.TournamentsAdmin
{
    [Service]
    public class DeleteTournament
    {
        private readonly ITournamentManager tournamentManager;

        public DeleteTournament(ITournamentManager tournamentManager)
        {
            this.tournamentManager = tournamentManager;
        }

        public Task<int> Do(int id) => tournamentManager.DeleteTournament(id);
    }
}
