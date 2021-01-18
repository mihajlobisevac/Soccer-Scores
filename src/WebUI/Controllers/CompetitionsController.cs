using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Competitions.Queries;
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
    }
}
