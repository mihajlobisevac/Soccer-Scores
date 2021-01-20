using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Admin.Clubs.Queries.GetClubQuery;
using SoccerScores.Application.Admin.Clubs.Queries.GetClubWithPlayersQuery;
using SoccerScores.Application.Admin.Clubs.Commands.CreateClub;
using SoccerScores.Application.Admin.Clubs.Commands.UpdateClub;

namespace SoccerScores.WebUI.Controllers
{
    public class ClubsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ClubDto>> Get(int id)
        {
            return await Mediator.Send(new GetClubQuery { Id = id });
        }

        [HttpGet("{id}/players")]
        public async Task<ActionResult<ClubWithPlayersDto>> GetWithPlayers(int id)
        {
            return await Mediator.Send(new GetClubWithPlayersQuery { Id = id });
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
    }
}
