using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Common.Models;
using SoccerScores.Application.Matches.Commands.CreateMatch;
using SoccerScores.Application.Matches.Commands.DeleteMatch;
using SoccerScores.Application.Matches.Commands.UpdateMatch;
using SoccerScores.Application.Matches.Queries.GetMatch;
using SoccerScores.Application.Matches.Queries.GetMatch.Models;
using SoccerScores.Application.Matches.Queries.GetMatchesByClub;
using SoccerScores.Application.Matches.Queries.GetMatchesByClub.Models;
using SoccerScores.Application.Matches.Queries.GetMatchesByDate;
using SoccerScores.Application.Matches.Queries.GetMatchesByDate.Models;
using SoccerScores.Application.Matches.Queries.GetMatchesBySeason;
using SoccerScores.Application.Matches.Queries.GetMatchesBySeason.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class MatchesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDto>> Get(int id)
        {
            return await Mediator.Send(new GetMatchQuery { Id = id });
        }

        [HttpGet("{year}-{month}-{day}")]
        public async Task<IEnumerable<MatchByDateDto>> GetByDate(int year, int month, int day)
        {
            return await Mediator.Send(new GetMatchesByDateQuery { Date = $"{year}/{month}/{day}" });
        }

        [HttpGet("club/{id}")]
        public async Task<PaginatedList<MatchByClubDto>> GetByClub(int id, bool futureMatches = false, int index = 1)
        {
            return await Mediator.Send(new GetMatchesByClubQuery { ClubId = id, PageNumber = index, IsFutureMatches = futureMatches });
        }

        [HttpGet("season/{id}")]
        public async Task<SeasonWithMatchesVm> GetBySeason(int id, int? gameWeek = null)
        {
            return await Mediator.Send(new GetMatchesBySeasonQuery { SeasonId = id, SpecifiedGameWeek = gameWeek });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateMatchCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateMatchCommand command)
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
            await Mediator.Send(new DeleteMatchCommand { Id = id });

            return NoContent();
        }
    }
}
