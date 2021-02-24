using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Competitions.Queries.GetCompetitions
{
    public class GetCompetitionsQuery : IRequest<IEnumerable<CountryWithCompetitionsDto>>
    {
    }

    public class GetCompetitionsQueryHandler : IRequestHandler<GetCompetitionsQuery, IEnumerable<CountryWithCompetitionsDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCompetitionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CountryWithCompetitionsDto>> Handle(GetCompetitionsQuery request, CancellationToken cancellationToken)
        {
            var countriesWithCompetitions = await context.Countries
                .Where(x => x.Competitions.Any())
                .Include(x => x.Competitions).ThenInclude(y => y.Seasons)
                .OrderBy(x => x.Name)
                .ProjectTo<CountryWithCompetitionsDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return countriesWithCompetitions;
        }
    }
}
