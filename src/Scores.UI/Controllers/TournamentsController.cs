using Microsoft.AspNetCore.Mvc;
using Scores.Application.TournamentsAdmin;
using System.Threading.Tasks;

namespace Scores.UI.Controllers
{
    [Route("[controller]")]
    public class TournamentsController : Controller
    {
        // GET: tournaments/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetTournamentById(int id, [FromServices] GetTournament getTournament)
            => Ok(getTournament.Do(id));

        // POST: tournaments/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournament.Request request, [FromServices] CreateTournament createTournament)
            => Ok(await createTournament.Do(request));

        // PUT: tournaments/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTournament([FromBody] UpdateTournament.Request request, [FromServices] UpdateTournament updateTournament)
            => Ok(await updateTournament.Do(request));
    }
}
