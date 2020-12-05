using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.CitiesAdmin
{
    [Service]
    public class DeleteCity
    {
        private readonly ICityManager cityManager;

        public DeleteCity(ICityManager cityManager)
        {
            this.cityManager = cityManager;
        }

        public Task<int> Do(int id) => cityManager.DeleteCity(id);
    }
}
