using FluentValidation;
using SoccerScores.Application.Common.Interfaces;

namespace SoccerScores.Application.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        private readonly IApplicationDbContext context;

        public CreateCountryCommandValidator(IApplicationDbContext context)
        {
            this.context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
