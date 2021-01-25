using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Common.Shared;
using System;

namespace SoccerScores.Application.Matches.Queries.GetMatchesByDate
{
    public class GetMatchesByDateQueryValidator : AbstractValidator<GetMatchesByDateQuery>
    {
        public GetMatchesByDateQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Date)
                .Must(CustomValidators.BeAValidDate).WithMessage("Invalid date/time format.");
        }
    }
}
