using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.EventsAdmin
{
    [Service]
    public class DeleteEvent
    {
        private readonly IEventManager eventManager;

        public DeleteEvent(IEventManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public Task<int> Do(int id) => eventManager.DeleteEvent(id);
    }
}
