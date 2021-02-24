using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Competitions.Queries.GetCompetitionsByCountry
{
    public class GetCompetitionsByCountryQuery : IRequest<IEnumerable<CompetitionDto>>
    {
        public int CountryId { get; set; }
    }

    public class GetCompetitionsByCountryQueryHandler : IRequestHandler<GetCompetitionsByCountryQuery, IEnumerable<CompetitionDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCompetitionsByCountryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CompetitionDto>> Handle(GetCompetitionsByCountryQuery request, CancellationToken cancellationToken)
        {
            return await context.Competitions
                .Where(x => x.Country.Id == request.CountryId)
                .ProjectTo<CompetitionDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
