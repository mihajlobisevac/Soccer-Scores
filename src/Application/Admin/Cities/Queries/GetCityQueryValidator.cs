using FluentValidation;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;

namespace SoccerScores.Application.Admin.Cities.Queries
{
    public class GetCityQueryValidator : AbstractValidator<GetCityQuery>
    {
        public GetCityQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(id => context.Cities.Any(c => c.Id == id)).WithMessage("City does not exist.");
        }
    }
}
