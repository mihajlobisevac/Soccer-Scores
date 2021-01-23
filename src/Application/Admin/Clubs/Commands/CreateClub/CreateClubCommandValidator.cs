using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Admin.Clubs.Commands.CreateClub
{
    public class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
    {
        public CreateClubCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.CityId)
                .NotEmpty()
                .Must(id => context.Cities.Any(c => c.Id == id)).WithMessage("City does not exist.");
        }
    }
}
