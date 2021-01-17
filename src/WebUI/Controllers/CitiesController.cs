using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Cities.Queries;
using SoccerScores.WebUI.Controllers;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class CitiesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            return await Mediator.Send(new GetCityQuery { Id = id });
        }
    }
}
