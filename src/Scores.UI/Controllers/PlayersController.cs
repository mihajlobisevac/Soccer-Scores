using Microsoft.AspNetCore.Mvc;
using Scores.Application.PlayersAdmin;
using System.Threading.Tasks;

namespace Scores.UI.Controllers
{
    [Route("[controller]")]
    public class PlayersController : Controller
    {
        // GET: players/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetPlayerById(int id, [FromServices] GetPlayer getPlayer)
            => Ok(getPlayer.Do(id));

        // POST: players/create
        [HttpPost("create")]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayer.Request request, [FromServices] CreatePlayer createPlayer)
            => Ok(await createPlayer.Do(request));

        // DELETE: players/delete/1
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePlayer(int id, [FromServices] DeletePlayer deletePlayer)
            => Ok(await deletePlayer.Do(id));

        // PUT: players/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePlayer([FromBody] UpdatePlayer.Request request, [FromServices] UpdatePlayer updatePlayer)
            => Ok(await updatePlayer.Do(request));
    }
}
