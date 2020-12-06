using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.MatchesAdmin
{
    [Service]
    public class DeleteMatch
    {
        private readonly IMatchManager matchManager;

        public DeleteMatch(IMatchManager matchManager)
        {
            this.matchManager = matchManager;
        }

        public Task<int> Do(int id) => matchManager.DeleteMatch(id);
    }
}
