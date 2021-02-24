using Domain.Enums;
using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System;
using System.Linq;

namespace SoccerScores.Application.Matches.Commands.Incidents.UpdateIncident
{
    public class UpdateIncidentCommandValidator : AbstractValidator<UpdateIncidentCommand>
    {
        public UpdateIncidentCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Incidents.Any(i => i.Id == id)).WithMessage("Chosen incident does not exist.");

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
