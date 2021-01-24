using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Clubs.Commands.UpdateClub
{
    public class UpdateClubCommandValidator : AbstractValidator<UpdateClubCommand>
    {
        public UpdateClubCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Clubs.Any(c => c.Id == id)).WithMessage("Club does not exist.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.CityId)
                .NotEmpty()
                .Must(id => context.Cities.Any(c => c.Id == id)).WithMessage("City does not exist.");
        }
    }
}
