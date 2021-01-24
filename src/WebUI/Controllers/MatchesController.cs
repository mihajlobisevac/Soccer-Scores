using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Matches.Commands.CreateMatch;
using SoccerScores.Application.Matches.Commands.DeleteMatch;
using SoccerScores.Application.Matches.Commands.UpdateMatch;
using SoccerScores.Application.Matches.Queries.GetMatch;
using SoccerScores.Application.Matches.Queries.GetMatch.Models;
using SoccerScores.Application.Matches.Queries.GetMatchesByDate;
using SoccerScores.Application.Matches.Queries.GetMatchesByDate.Models;
using System.Collections.Generic;
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

        [HttpGet("{year}-{month}-{day}")]
        public async Task<ICollection<MatchByDateDto>> Get(int year, int month, int day)
        {
            return await Mediator.Send(new GetMatchesByDateQuery
            {
                Year = year,
                Month = month,
                Day = day,
            });
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteMatchCommand { Id = id });

            return NoContent();
        }
    }
}
