using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Matches.Commands.Players.UpdateMatchPlayer
{
    public class UpdateMatchPlayerCommandValidator : AbstractValidator<UpdateMatchPlayerCommand>
    {
        public UpdateMatchPlayerCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.MatchPlayers.Any(i => i.Id == id)).WithMessage("Chosen match player does not exist.");

            RuleFor(x => x.IsHome)
                .NotNull();

            RuleFor(x => x.IsSubstitute)
                .NotNull();

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .Must(id => context.Players.Any(i => i.Id == id)).WithMessage("Chosen player does not exist.");
        }
    }
}
