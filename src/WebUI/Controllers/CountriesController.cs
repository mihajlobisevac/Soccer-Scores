using SoccerScores.Application.Admin.Countries.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class CountriesController : ApiControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<CountriesVm>> Get()
        {
            return await Mediator.Send(new GetCountriesQuery());
        }
    }
}
