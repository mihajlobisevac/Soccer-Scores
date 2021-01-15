using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Countries.Queries
{
    public class GetCountriesQuery : IRequest<CountriesVm>
    {
    }

    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, CountriesVm>
    {
        private readonly IApplicationDbContext context;

        public GetCountriesQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<CountriesVm> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            return new CountriesVm
            {
                Countries = await context.Countries
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
