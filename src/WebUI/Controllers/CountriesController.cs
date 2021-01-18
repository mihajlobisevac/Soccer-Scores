using SoccerScores.Application.Admin.Countries.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Admin.Countries.Commands.CreateCountry;
using SoccerScores.Application.Admin.Countries.Commands.DeleteCountry;

namespace SoccerScores.WebUI.Controllers
{
    public class CountriesController : ApiControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<CountriesVm>> Get()
        {
            return await Mediator.Send(new GetCountriesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> Get(int id)
        {
            return await Mediator.Send(new GetCountryQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCountryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCountryCommand { Id = id });

            return NoContent();
        }
    }
}
