using Domain.Enums;
using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Leagues.Queries.GetLeagueTable
{
    public class GetLeagueTableQueryValidator : AbstractValidator<GetLeagueTableQuery>
    {
        private readonly IApplicationDbContext context;

        public GetLeagueTableQueryValidator(IApplicationDbContext context)
        {
            this.context = context;

            RuleFor(x => x.SeasonId)
                .NotEmpty()
                .Must(BeLeagueType).WithMessage("Season must be a league type of competition.");
        }

        bool BeLeagueType(int id)
        {
            return context.Seasons
                .Where(x => x.Id == id)
                .Where(x => x.Competition.Type == CompetitionType.League)
                .Any();
        }
    }
}
