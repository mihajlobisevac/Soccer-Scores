using Microsoft.AspNetCore.Mvc;
using Scores.Application.MatchesAdmin;
using System.Threading.Tasks;

namespace Scores.UI.Controllers
{
    [Route("[controller]")]
    public class MatchesController : Controller
    {
        // GET: matches/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetMatchById(int id, [FromServices] GetMatch getMatch)
            => Ok(getMatch.Do(id));

        // POST: matches/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatch.Request request, [FromServices] CreateMatch createMatch)
            => Ok(await createMatch.Do(request));

        // PUT: matches/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity([FromBody] UpdateMatch.Request request, [FromServices] UpdateMatch updateMatch)
            => Ok(await updateMatch.Do(request));
    }
}
