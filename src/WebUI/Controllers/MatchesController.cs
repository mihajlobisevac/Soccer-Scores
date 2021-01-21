using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Matches.Commands.CreateMatch;
using SoccerScores.Application.Admin.Matches.Commands.UpdateMatch;
using SoccerScores.Application.Admin.Matches.Queries.GetMatch;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class MatchesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDto>> Get(int id)
        {
            return await Mediator.Send(new GetMatchQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateMatchCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateMatchCommand command)
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
