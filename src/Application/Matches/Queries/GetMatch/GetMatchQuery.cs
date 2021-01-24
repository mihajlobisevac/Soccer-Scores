using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Matches.Queries.GetMatch.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Queries.GetMatch
{
    public class GetMatchQuery : IRequest<MatchDto>
    {
        public int Id { get; set; }
    }

    public class GetMatchQueryHandler : IRequestHandler<GetMatchQuery, MatchDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<MatchDto> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Season).ThenInclude(x => x.Competition)
                .Include(x => x.Incidents).ThenInclude(x => x.PrimaryPlayer)
                .Include(x => x.Incidents).ThenInclude(x => x.SecondaryPlayer)
                .Include(x => x.Players)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<MatchDto>(entity);
        }
    }
}
