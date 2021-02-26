using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Countries.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<IEnumerable<CountryDto>>
    {
    }

    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<CountryDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCountriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            return await context.Countries
                .ProjectTo<CountryDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
