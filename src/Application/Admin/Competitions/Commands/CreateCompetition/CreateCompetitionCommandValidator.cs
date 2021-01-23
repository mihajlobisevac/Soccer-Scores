using Domain.Enums;
using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System;
using System.Linq;

namespace SoccerScores.Application.Admin.Competitions.Commands.CreateCompetition
{
    public class CreateCompetitionCommandValidator : AbstractValidator<CreateCompetitionCommand>
    {
        public CreateCompetitionCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Type)
                .NotEmpty()
                .Must(type => Enum.IsDefined(typeof(CompetitionType), type)).WithMessage("Competition type is invalid.");

            RuleFor(x => x.CountryId)
                .NotEmpty()
                .Must(id => context.Countries.Any(c => c.Id == id)).WithMessage("Country does not exist.");
        }
    }
}
