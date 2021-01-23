using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Admin.Competitions.Commands.UpdateCompetition
{
    public class UpdateCompetitionCommandValidator : AbstractValidator<UpdateCompetitionCommand>
    {
        public UpdateCompetitionCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Competitions.Any(c => c.Id == id)).WithMessage("Competition does not exist.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
