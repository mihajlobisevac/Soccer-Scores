using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Cities.Queries.GetCity;
using SoccerScores.Application.Cities.Queries.GetCities;
using SoccerScores.Application.Cities.Commands.CreateCity;
using SoccerScores.Application.Cities.Commands.UpdateCity;
using SoccerScores.Application.Cities.Commands.DeleteCity;
using System.Collections.Generic;
using Country = SoccerScores.Application.Cities.Queries.GetCitiesByCountry;

namespace SoccerScores.WebUI.Controllers
{
    public class CitiesController : ApiControllerBase
    {
        [HttpGet("all")]
        public async Task<IEnumerable<CountryWithCitiesDto>> Get()
        {
            return await Mediator.Send(new GetCitiesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            return await Mediator.Send(new GetCityQuery { Id = id });
        }

        [HttpGet("country/{countryId}")]
        public async Task<IEnumerable<Country.CityDto>> GetByCountry(int countryId)
        {
            return await Mediator.Send(new Country.GetCitiesByCountryQuery { CountryId = countryId });
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCityCommand { Id = id });

            return NoContent();
        }
    }
}
