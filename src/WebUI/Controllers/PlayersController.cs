using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerScores.Application.Common.Models;
using SoccerScores.Application.Players.Commands.CreatePlayer;
using SoccerScores.Application.Players.Commands.DeletePlayer;
using SoccerScores.Application.Players.Commands.UpdatePlayer;
using SoccerScores.Application.Players.Queries.GetPlayer;
using SoccerScores.Application.Players.Queries.GetPlayerMatches;
using Club = SoccerScores.Application.Players.Queries.GetPlayersByClub;
using SoccerScores.Application.Players.Queries.GetRawPlayer;

namespace SoccerScores.WebUI.Controllers
{
    public class PlayersController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> Get(int id)
        {
            return await Mediator.Send(new GetPlayerQuery { Id = id });
        }

        [HttpGet("{id}/raw")]
        public async Task<ActionResult<RawPlayerDto>> GetRaw(int id)
        {
            return await Mediator.Send(new GetRawPlayerQuery { Id = id });
        }

        [HttpGet("{playerId}/matches")]
        public async Task<PaginatedList<PlayerMatchDto>> GetMatches(int playerId, int index = 1)
        {
            return await Mediator.Send(new GetPlayerMatchesQuery { PlayerId = playerId, PageNumber = index });
        }

        [HttpGet("club/{clubId}")]
        public async Task<IEnumerable<Club.PlayerDto>> GetWithPlayers(int clubId)
        {
            return await Mediator.Send(new Club.GetPlayersByClubQuery { ClubId = clubId });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePlayerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePlayerCommand command)
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
            await Mediator.Send(new DeletePlayerCommand { Id = id });

            return NoContent();
        }
    }
}
