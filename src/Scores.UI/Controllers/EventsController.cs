using Microsoft.AspNetCore.Mvc;
using Scores.Application.EventsAdmin;
using System.Threading.Tasks;

namespace Scores.UI.Controllers
{
    [Route("[controller]")]
    public class EventsController : Controller
    {
        // GET: events/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetEventById(int id, [FromServices] GetEvent getEvent)
            => Ok(getEvent.Do(id));

        // POST: events/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEvent.Request request, [FromServices] CreateEvent createEvent)
            => Ok(await createEvent.Do(request));

        // DELETE: events/delete/1
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEvent(int id, [FromServices] DeleteEvent deleteEvent)
            => Ok(await deleteEvent.Do(id));

        // PUT: events/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEvent.Request request, [FromServices] UpdateEvent updateEvent)
            => Ok(await updateEvent.Do(request));
    }
}
