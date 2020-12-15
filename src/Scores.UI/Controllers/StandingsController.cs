using Microsoft.AspNetCore.Mvc;
using Scores.Application.StandingsAdmin;
using System.Threading.Tasks;

namespace Scores.UI.Controllers
{
    [Route("[controller]")]
    public class StandingsController : Controller
    {
        // GET: standings/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetStandingsById(int id, [FromServices] GetStandings getStandings)
            => Ok(getStandings.Do(id));

        // POST: standings/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateStandings([FromBody] CreateStandings.Request request, [FromServices] CreateStandings createStandings)
            => Ok(await createStandings.Do(request));

        // PUT: standings/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStandings([FromBody] UpdateStandings.Request request, [FromServices] UpdateStandings updateStandings)
            => Ok(await updateStandings.Do(request));

        // POST: standings/addClub
        [HttpPost("addClub")]
        public async Task<IActionResult> AddClubToStandings([FromBody] AddClubToStandings.Request request, [FromServices] AddClubToStandings addClub)
            => Ok(await addClub.Do(request));

        // DELETE: standings/removeClub/1
        [HttpDelete("removeClub/{clubStandingsId}")]
        public async Task<IActionResult> RemoveClubFromStandings(int clubStandingsId, [FromServices] RemoveClubFromStandings removeClub)
            => Ok(await removeClub.Do(clubStandingsId));
    }
}
