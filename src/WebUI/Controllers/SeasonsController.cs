using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SoccerScores.Application.Leagues.Queries.GetLeagueTable;
using SoccerScores.Application.Seasons.Commands.CreateSeason;
using SoccerScores.Application.Seasons.Commands.DeleteSeason;
using SoccerScores.Application.Seasons.Queries.GetSeason;
using Competition = SoccerScores.Application.Seasons.Queries.GetSeasonsByCompetition;
using System.Collections.Generic;

namespace SoccerScores.WebUI.Controllers
{
    public class SeasonsController : ApiControllerBase
    {
        [HttpGet("competition/{id}")]
        public async Task<IEnumerable<Competition.SeasonDto>> GetByCompetition(int id)
        {
            return await Mediator.Send(new Competition.GetSeasonsByCompetitionQuery { CompetitionId = id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonDto>> Get(int id)
        {
            return await Mediator.Send(new GetSeasonQuery { Id = id });
        }

        [HttpGet("{id}/league/table")]
        public async Task<ActionResult<LeagueTable>> GetTable(int id)
        {
            return await Mediator.Send(new GetLeagueTableQuery { SeasonId = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSeasonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSeasonCommand { Id = id });

            return NoContent();
        }
    }
}
