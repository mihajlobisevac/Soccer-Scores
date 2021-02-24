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
            RuleFor(x => x.Type)
                .NotEmpty()
                .Must(type => Enum.IsDefined(typeof(IncidentType), type)).WithMessage("Incident type is invalid.");

            RuleFor(x => x.Class)
                .NotEmpty()
                .Must(incidentClass => Enum.IsDefined(typeof(IncidentClass), incidentClass)).WithMessage("Incident class is invalid.");

            RuleFor(x => x.IsHome)
                .NotNull();

            RuleFor(x => x.MatchId)
                .NotEmpty()
                .Must(id => context.Matches.Any(m => m.Id == id)).WithMessage("Match does not exist.");
        }
    }
}
