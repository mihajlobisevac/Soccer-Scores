using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Common.Shared;
using System;
using System.Linq;

namespace SoccerScores.Application.Matches.Commands.UpdateMatch
{
    public class UpdateMatchCommandValidator : AbstractValidator<UpdateMatchCommand>
    {
        public UpdateMatchCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Matches.Any(m => m.Id == id)).WithMessage("Match does not exist.");

            RuleFor(x => x.KickOff)
                .Must(CustomValidators.BeAValidDate).WithMessage("Invalid date/time format.");
        }
    }
}
