using FluentValidation;

namespace SoccerScores.Application.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
