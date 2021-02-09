using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Competitions.Queries;
using SoccerScores.Application.Competitions.Commands.UpdateCompetition;
using SoccerScores.Application.Competitions.Commands.CreateCompetition;
using SoccerScores.Application.Competitions.Commands.DeleteCompetition;
using SoccerScores.Application.Competitions.Queries.GetCompetition;
using System.Collections.Generic;
using SoccerScores.Application.Competitions.Queries.GetCompetitions;

namespace SoccerScores.WebUI.Controllers
{
    public class CompetitionsController : ApiControllerBase
    {
        [HttpGet("all")]
        public async Task<IEnumerable<CountryWithCompetitionsDto>> Get()
        {
            return await Mediator.Send(new GetCompetitionsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompetitionDto>> Get(int id)
        {
            return await Mediator.Send(new GetCompetitionQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCompetitionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCompetitionCommand command)
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
            await Mediator.Send(new DeleteCompetitionCommand { Id = id });

            return NoContent();
        }
    }
}
