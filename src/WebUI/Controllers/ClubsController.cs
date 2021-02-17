using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using SoccerScores.Application.Clubs.Queries.GetClub;
using SoccerScores.Application.Clubs.Queries.GetClubPlayers;
using SoccerScores.Application.Clubs.Commands.CreateClub;
using SoccerScores.Application.Clubs.Commands.UpdateClub;
using SoccerScores.Application.Clubs.Commands.DeleteClub;
using Country = SoccerScores.Application.Clubs.Queries.GetClubsByCountry;

namespace SoccerScores.WebUI.Controllers
{
    public class ClubsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubDto>> Get(int id)
        {
            return await Mediator.Send(new GetClubQuery { Id = id });
        }

        [HttpGet("{clubId}/players")]
        public async Task<IEnumerable<ClubPlayerDto>> GetClubPlayers(int clubId)
        {
            return await Mediator.Send(new GetClubPlayersQuery { ClubId = clubId });
        }

        [HttpGet("country/{countryId}")]
        public async Task<IEnumerable<Country.ClubDto>> GetClubsByCountry(int countryId)
        {
            return await Mediator.Send(new Country.GetClubsByCountryQuery { CountryId = countryId });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateClubCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateClubCommand command)
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
            await Mediator.Send(new DeleteClubCommand { Id = id });

            return NoContent();
        }
    }
}
