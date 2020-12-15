using Microsoft.AspNetCore.Mvc;
using Scores.Application.ClubsAdmin;
using System.Threading.Tasks;

namespace Scores.UI.Controllers
{
    [Route("[controller]")]
    public class ClubsController : Controller
    {
        // GET: clubs/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetClubById(int id, [FromServices] GetClub getClub)
            => Ok(getClub.Do(id));

        // POST: clubs/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateClub([FromBody] CreateClub.Request request, [FromServices] CreateClub createClub)
            => Ok(await createClub.Do(request));

        // PUT: clubs/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateClub([FromBody] UpdateClub.Request request, [FromServices] UpdateClub updateClub)
            => Ok(await updateClub.Do(request));
    }
}
