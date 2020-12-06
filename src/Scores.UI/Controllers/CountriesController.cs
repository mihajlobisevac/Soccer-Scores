using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scores.Application.CountriesAdmin;

namespace Scores.UI.Controllers
{
    [Route("[controller]")]
    public class CountriesController : Controller
    {
        // GET: countries/get/1
        [HttpGet("get/{id}")]
        public IActionResult GetCountryById(int id, [FromServices] GetCountry getCountry)
            => Ok(getCountry.Do(id));

        // POST: countries/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountry.Request request, [FromServices] CreateCountry createCountry)
            => Ok(await createCountry.Do(request));

        // DELETE: countries/delete/1
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCountry(int id, [FromServices] DeleteCountry deleteCountry)
            => Ok(await deleteCountry.Do(id));

        // PUT: countries/update/1
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity([FromBody] UpdateCountry.Request request, [FromServices] UpdateCountry updateCountry)
            => Ok(await updateCountry.Do(request));
    }
}
