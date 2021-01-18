using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Cities.Queries
{
    public class GetCitiesQuery : IRequest<CitiesVm>
    {
    }

    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, CitiesVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CitiesVm> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            return new CitiesVm
            {
                Cities = await context.Cities
                    .Include(x => x.Country)
                    .OrderBy(x => x.Country)
                    .ProjectTo<CityDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
