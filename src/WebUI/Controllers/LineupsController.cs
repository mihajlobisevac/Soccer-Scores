using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Matches.Commands.Players.UpdateMatchPlayer;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class LineupsController : ApiControllerBase
    {
        [HttpPut("player/{id}")]
        public async Task<ActionResult> Update(int id, UpdateMatchPlayerCommand command)
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
