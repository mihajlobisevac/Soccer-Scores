using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Cities.Commands.CreateCity;
using SoccerScores.Application.Admin.Cities.Queries;
using SoccerScores.WebUI.Controllers;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class CitiesController : ApiControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<CitiesVm>> Get()
        {
            return await Mediator.Send(new GetCitiesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            return await Mediator.Send(new GetCityQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(int id, CreateCityCommand command)
        {
            if (id != command.CountryId)
            {
                return BadRequest();
            }

            return await Mediator.Send(command);
        }
    }
}
