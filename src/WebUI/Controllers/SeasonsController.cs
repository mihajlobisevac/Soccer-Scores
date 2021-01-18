using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Admin.Seasons.Queries;

namespace SoccerScores.WebUI.Controllers
{
    public class SeasonsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonDto>> Get(int id)
        {
            return await Mediator.Send(new GetSeasonQuery { Id = id });
        }
    }
}
