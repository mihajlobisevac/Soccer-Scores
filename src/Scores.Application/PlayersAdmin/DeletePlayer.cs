using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.PlayersAdmin
{
    [Service]
    public class DeletePlayer
    {
        private readonly IPlayerManager playerManager;

        public DeletePlayer(IPlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public Task<int> Do(int id) => playerManager.DeletePlayer(id);
    }
}
