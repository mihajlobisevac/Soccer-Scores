using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;
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
        private readonly IMapper mapper;

        public GetCountriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CountriesVm> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            return new CountriesVm
            {
                Countries = await context.Countries
                    .Select(x => new CountryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Flag = x.Flag,
                        Test = "Yo",
                        Cities = x.Cities
                            .Select(x => x.Name)
                            .ToList(),
                    })
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
