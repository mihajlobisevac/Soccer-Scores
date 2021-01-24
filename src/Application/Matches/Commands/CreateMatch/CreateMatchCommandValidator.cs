using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System;
using System.Linq;

namespace SoccerScores.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        public CreateMatchCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.KickOff)
                .Must(BeAValidDate).WithMessage("Invalid date/time format.");

            RuleFor(x => x.HomeTeamId)
                .NotEmpty()
                .NotEqual(x => x.AwayTeamId).WithMessage("Teams must not be the same.")
                .Must(id => context.Clubs.Any(c => c.Id == id)).WithMessage("Chosen home team does not exist.");

            RuleFor(x => x.AwayTeamId)
                .NotEmpty()
                .Must(id => context.Clubs.Any(c => c.Id == id)).WithMessage("Chosen away team does not exist.");

            RuleFor(x => x.SeasonId)
                .NotEmpty()
                .Must(id => context.Seasons.Any(s => s.Id == id)).WithMessage("Competition season does not exist.");
        }

        private bool BeAValidDate(string value)
        {
            return DateTime.TryParse(value, out _);
        }
    }
}
