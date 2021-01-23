using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System;
using System.Linq;

namespace SoccerScores.Application.Admin.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommandValidator : AbstractValidator<CreateSeasonCommand>
    {
        public CreateSeasonCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Start)
                .Must(BeAValidDate).WithMessage("Invalid date/time format.");

            RuleFor(x => x.End)
                .Must(BeAValidDate).WithMessage("Invalid date/time format.");

            RuleFor(x => x.CompetitionId)
                .NotEmpty()
                .Must(id => context.Competitions.Any(c => c.Id == id)).WithMessage("Competition does not exist.");
        }

        private bool BeAValidDate(string value)
        {
            return DateTime.TryParse(value, out _);
        }
    }
}
