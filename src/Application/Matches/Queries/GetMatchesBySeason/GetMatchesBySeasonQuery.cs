using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Matches.Queries.GetMatchesBySeason.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Queries.GetMatchesBySeason
{
    public class GetMatchesBySeasonQuery : IRequest<PaginatedList<MatchBySeasonDto>>
    {
        public int SeasonId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public bool IsFutureMatches { get; set; }
    }

    public class GetMatchesBySeasonQueryHandler : IRequestHandler<GetMatchesBySeasonQuery, PaginatedList<MatchBySeasonDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchesBySeasonQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<MatchBySeasonDto>> Handle(GetMatchesBySeasonQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<MatchBySeasonDto> matches;

            if (request.IsFutureMatches)
            {
                matches = await context.Matches
                    .Where(x => x.KickOff >= DateTime.Now)
                    .Where(x => x.Season.Id == request.SeasonId)
                    .OrderBy(x => x.KickOff)
                    .ProjectTo<MatchBySeasonDto>(mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);

                return matches;
            }

            matches = await context.Matches
                .Where(x => x.KickOff < DateTime.Now)
                .Where(x => x.Season.Id == request.SeasonId)
                .OrderByDescending(x => x.KickOff)
                .ProjectTo<MatchBySeasonDto>(mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return matches;
        }
    }
}
