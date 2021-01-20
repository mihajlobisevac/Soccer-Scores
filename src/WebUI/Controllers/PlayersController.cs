using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Players.Queries.GetPlayer;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class PlayersController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> Get(int id)
        {
            return await Mediator.Send(new GetPlayerQuery { Id = id });
        }
    }
}
