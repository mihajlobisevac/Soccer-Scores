using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Matches.Commands.Players.CreateMatchPlayer
{
    public class CreateMatchPlayerCommandValidator : AbstractValidator<CreateMatchPlayerCommand>
    {
        public CreateMatchPlayerCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.IsHome)
                .NotNull();

            RuleFor(x => x.IsSubstitute)
                .NotNull();

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .Must(id => context.Players.Any(i => i.Id == id)).WithMessage("Chosen player does not exist.");

            RuleFor(x => x.MatchId)
                .NotEmpty()
                .Must(id => context.Matches.Any(i => i.Id == id)).WithMessage("Chosen match does not exist.");
        }
    }
}
