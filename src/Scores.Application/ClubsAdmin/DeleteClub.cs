using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.ClubsAdmin
{
    [Service]
    public class DeleteClub
    {
        private readonly IClubManager clubManager;

        public DeleteClub(IClubManager clubManager)
        {
            this.clubManager = clubManager;
        }

        public Task<int> Do(int id) => clubManager.DeleteClub(id);
    }
}
