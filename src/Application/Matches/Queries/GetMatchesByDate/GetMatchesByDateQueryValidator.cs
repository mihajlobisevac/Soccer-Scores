using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System;

namespace SoccerScores.Application.Matches.Queries.GetMatchesByDate
{
    public class GetMatchesByDateQueryValidator : AbstractValidator<GetMatchesByDateQuery>
    {
        public GetMatchesByDateQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Date)
                .Must(BeAValidDate).WithMessage("Invalid date/time format.");
        }

        private bool BeAValidDate(string value)
        {
            return DateTime.TryParse(value, out _);
        }
    }
}
