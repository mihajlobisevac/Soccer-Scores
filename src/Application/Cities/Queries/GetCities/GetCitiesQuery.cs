using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Cities.Queries.GetCities
{
    public class GetCitiesQuery : IRequest<IEnumerable<CountryWithCitiesDto>>
    {
    }

    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IEnumerable<CountryWithCitiesDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CountryWithCitiesDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            return await context.Countries
                .Where(x => x.Cities.Any())
                .Include(x => x.Cities)
                .ProjectTo<CountryWithCitiesDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
