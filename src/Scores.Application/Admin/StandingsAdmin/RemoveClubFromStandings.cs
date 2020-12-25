using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.StandingsAdmin
{
    [Service]
    public class RemoveClubFromStandings
    {
        private readonly IStandingsManager standingsManager;

        public RemoveClubFromStandings(IStandingsManager standingsManager)
        {
            this.standingsManager = standingsManager;
        }

        public Task<int> Do(int id) => standingsManager.RemoveClub(id);
    }
}
