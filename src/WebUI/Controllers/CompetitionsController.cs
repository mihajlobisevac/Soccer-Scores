using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Competitions.Queries;
using SoccerStreams.Application.Admin.Competitions.Commands.CreateCompetition;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class CompetitionsController : ApiControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<CompetitionsVm>> Get()
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
    }
}
