using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Players.Commands.CreatePlayer;
using SoccerScores.Application.Admin.Players.Queries.GetPlayer;
using SoccerScores.Application.Admin.Players.Queries.GetPlayersByClub;
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

        [HttpGet("club/{id}")]
        public async Task<ActionResult<PlayersVm>> GetWithPlayers(int id)
        {
            return await Mediator.Send(new GetPlayersByClubQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePlayerCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
