using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Admin.Cities.Queries;
using SoccerScores.Application.Admin.Cities.Commands.CreateCity;
using SoccerScores.Application.Admin.Cities.Commands.UpdateCity;

namespace SoccerScores.WebUI.Controllers
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
        public async Task<ActionResult<int>> Create(CreateCityCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCityCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
