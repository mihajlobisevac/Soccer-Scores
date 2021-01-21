using Microsoft.AspNetCore.Mvc;
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
    }
}
