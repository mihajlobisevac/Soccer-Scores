using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Players.Commands.RemoveFromClub
{
    public class RemoveFromClubCommandValidator : AbstractValidator<RemoveFromClubCommand>
    {
        public RemoveFromClubCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Players.Any(c => c.Id == id)).WithMessage("Player with id: {id} doesn't exist.")
                .Must(id => context.ClubPlayers.Any(c => c.Player.Id == id)).WithMessage("Player with id: {id} doesn't have a club.");
        }
    }
}
