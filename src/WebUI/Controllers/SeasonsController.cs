using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Admin.Seasons.Queries;

namespace SoccerScores.WebUI.Controllers
{
    public class SeasonsController : ApiControllerBase
    {
        [HttpGet("competition/{id}")]
        public async Task<ActionResult<SeasonsVm>> GetByCompetition(int id)
        {
            return await Mediator.Send(new GetSeasonsByCompetitionQuery { CompetitionId = id });
        }

        [HttpGet("season/{id}")]
        public async Task<ActionResult<SeasonDto>> Get(int id)
        {
            return await Mediator.Send(new GetSeasonQuery { Id = id });
        }
    }
}
