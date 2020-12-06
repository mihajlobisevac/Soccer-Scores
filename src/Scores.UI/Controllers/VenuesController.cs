using Microsoft.AspNetCore.Mvc;
using Scores.Application.VenuesAdmin;
using System.Threading.Tasks;

namespace Scores.UI.Controllers
{
    public class VenuesController : Controller
    {
        // GET: venues/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetVenueById(int id, [FromServices] GetVenue getVenue)
            => Ok(getVenue.Do(id));

        // POST: venues/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateVenue([FromBody] CreateVenue.Request request, [FromServices] CreateVenue createVenue)
            => Ok(await createVenue.Do(request));

        // DELETE: venues/delete/1
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVenue(int id, [FromServices] DeleteVenue deleteVenue)
            => Ok(await deleteVenue.Do(id));

        // PUT: venues/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVenue([FromBody] UpdateVenue.Request request, [FromServices] UpdateVenue updateVenue)
            => Ok(await updateVenue.Do(request));
    }
}
