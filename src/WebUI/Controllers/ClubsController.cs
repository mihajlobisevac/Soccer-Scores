using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Admin.Clubs.Queries;

namespace SoccerScores.WebUI.Controllers
{
    public class ClubsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubDto>> Get(int id)
        {
            return await Mediator.Send(new GetClubQuery { Id = id });
        }
    }
}
