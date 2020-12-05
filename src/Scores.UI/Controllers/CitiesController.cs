using Microsoft.AspNetCore.Mvc;
using Scores.Application.CitiesAdmin;
using System.Threading.Tasks;

namespace Scores.UI.Controllers
{
    [Route("[controller]")]
    public class CitiesController : Controller
    {
        // GET cities/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetCityById(int id, [FromServices] GetCity getCity)
            => Ok(getCity.Do(id));

        // POST cities/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateCity([FromBody] CreateCity.Request request, [FromServices] CreateCity createCity)
            => Ok(await createCity.Do(request));
    }
}
