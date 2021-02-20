using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Queries.Incidents
{
    public class GetMatchIncidentsQuery : IRequest<IEnumerable<IncidentDto>>
    {
        public int MatchId { get; set; }
    }

    public class GetMatchIncidentsQueryHandler : IRequestHandler<GetMatchIncidentsQuery, IEnumerable<IncidentDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchIncidentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<IncidentDto>> Handle(GetMatchIncidentsQuery request, CancellationToken cancellationToken)
        {
            return await context.Incidents
                .Where(x => x.Match.Id == request.MatchId)
                .ProjectTo<IncidentDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
