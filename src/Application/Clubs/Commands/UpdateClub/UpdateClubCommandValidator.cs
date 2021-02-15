using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Clubs.Commands.UpdateClub
{
    public class UpdateClubCommandValidator : AbstractValidator<UpdateClubCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateClubCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Clubs.Any(c => c.Id == id)).WithMessage("Club does not exist.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.CityId)
                .Must(id => BeAValidCityIfNotNull(id)).WithMessage("City does not exist.");
        }

        private bool BeAValidCityIfNotNull(int? id)
        {
            if (id is null || id == 0)
            {
                return true;
            }

            return context.Cities.Any(c => c.Id == id);
        }
    }
}
