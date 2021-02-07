using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Players.Queries.GetPlayerMatches
{
    public class GetPlayerMatchesQuery : IRequest<PaginatedList<PlayerMatchDto>>
    {
        public int PlayerId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }

    public class GetPlayerMatchesQueryHandler : IRequestHandler<GetPlayerMatchesQuery, PaginatedList<PlayerMatchDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetPlayerMatchesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<PlayerMatchDto>> Handle(GetPlayerMatchesQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<PlayerMatchDto> matches;

            matches = await context.Matches
                .Where(x => x.Players.Any(p => p.Player.Id == request.PlayerId))
                .OrderByDescending(x => x.KickOff)
                .ProjectTo<PlayerMatchDto>(mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return matches;
        }
    }
}
