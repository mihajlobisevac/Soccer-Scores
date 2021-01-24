using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Cities.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.CountryId)
                .NotEmpty()
                .Must(id => context.Countries.Any(c => c.Id == id)).WithMessage("Country does not exist.");
        }
    }
}
