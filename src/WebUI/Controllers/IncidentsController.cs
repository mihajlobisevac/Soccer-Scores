using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Matches.Commands.CreateIncidents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class IncidentsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<IEnumerable<int>>> Create(BatchCreateIncidentsCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
