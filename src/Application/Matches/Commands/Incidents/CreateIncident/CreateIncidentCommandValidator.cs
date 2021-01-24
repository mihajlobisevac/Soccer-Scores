using Domain.Enums;
using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System;
using System.Linq;

namespace SoccerScores.Application.Matches.Commands.Incidents.CreateIncident
{
    public class CreateIncidentCommandValidator : AbstractValidator<CreateIncidentCommand>
    {
        public CreateIncidentCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Incident.HomeScore)
                .NotEmpty();

            RuleFor(x => x.Incident.AwayScore)
                .NotEmpty();

            RuleFor(x => x.Incident.Type)
                .NotEmpty()
                .Must(type => Enum.IsDefined(typeof(IncidentType), type)).WithMessage("Incident type is invalid.");

            RuleFor(x => x.Incident.Class)
                .NotEmpty()
                .Must(incidentClass => Enum.IsDefined(typeof(IncidentClass), incidentClass)).WithMessage("Incident class is invalid.");

            RuleFor(x => x.Incident.IsHome)
                .NotNull();

            RuleFor(x => x.Incident.MatchId)
                .NotEmpty()
                .Must(id => context.Matches.Any(m => m.Id == id)).WithMessage("Match does not exist.");
        }
    }
}
