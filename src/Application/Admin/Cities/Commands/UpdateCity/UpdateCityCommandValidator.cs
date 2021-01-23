using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Admin.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Cities.Any(c => c.Id == id)).WithMessage("City does not exist.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
