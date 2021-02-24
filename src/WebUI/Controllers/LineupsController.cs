using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Matches.Commands.Players.CreateMatchPlayer;
using SoccerScores.Application.Matches.Commands.Players.UpdateMatchPlayer;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class LineupsController : ApiControllerBase
    {

        [HttpPost("player")]
        public async Task<ActionResult<int>> CreatePlayer(CreateMatchPlayerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("player/{id}")]
        public async Task<ActionResult> UpdatePlayer(int id, UpdateMatchPlayerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
