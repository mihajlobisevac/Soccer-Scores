using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Common.Shared;
using System;
using System.Linq;

namespace SoccerScores.Application.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
    {
        public CreateSeasonCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Start)
                .Must(CustomValidators.BeAValidDate).WithMessage("Invalid date/time format.");

            RuleFor(x => x.End)
                .Must(CustomValidators.BeAValidDate).WithMessage("Invalid date/time format.");

            RuleFor(x => x.CompetitionId)
                .NotEmpty()
                .Must(id => context.Competitions.Any(c => c.Id == id)).WithMessage("Competition does not exist.");
        }
    }
}
