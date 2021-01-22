﻿using Microsoft.AspNetCore.Mvc;
using SoccerScores.Application.Admin.Matches.Commands.Incidents.CreateIncidents;
using SoccerScores.Application.Admin.Matches.Commands.Incidents.DeleteIncident;
using SoccerScores.Application.Admin.Matches.Commands.Incidents.UpdateIncident;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerScores.WebUI.Controllers
{
    public class IncidentsController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateIncidentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<int>>> Create(BatchCreateIncidentsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateIncidentCommand command)
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
            await Mediator.Send(new DeleteIncidentCommand { Id = id });

            return NoContent();
        }
    }
}
