using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Admin.Clubs.Queries.GetClub;
using SoccerScores.Application.Admin.Clubs.Queries.GetClubWithPlayers;
using SoccerScores.Application.Admin.Clubs.Commands.CreateClub;
using SoccerScores.Application.Admin.Clubs.Commands.UpdateClub;
using SoccerScores.Application.Admin.Clubs.Commands.DeleteClub;

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteClubCommand { Id = id });

            return NoContent();
        }
    }
}
