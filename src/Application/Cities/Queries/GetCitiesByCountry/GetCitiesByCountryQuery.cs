using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Cities.Queries.GetCitiesByCountry
{
    public class GetCitiesByCountryQuery : IRequest<IEnumerable<CityDto>>
    {
        public int CountryId { get; set; }
    }

    public class GetCitiesByCountryQueryHandler : IRequestHandler<GetCitiesByCountryQuery, IEnumerable<CityDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCitiesByCountryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CityDto>> Handle(GetCitiesByCountryQuery request, CancellationToken cancellationToken)
        {
            return await context.Cities
                .Where(x => x.Country.Id == request.CountryId)
                .ProjectTo<CityDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
