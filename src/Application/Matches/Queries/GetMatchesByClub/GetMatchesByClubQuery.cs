using SoccerScores.Application.Common.Mappings;
using SoccerScores.Application.Common.Models;
using SoccerScores.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Queries.GetMatchesByClub
{
    public class GetMatchesByClubQuery : IRequest<PaginatedList<MatchByClubDto>>
    {
        public int ClubId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public bool IsFutureMatches { get; set; }
    }

    public class GetMatchesByClubQueryHandler : IRequestHandler<GetMatchesByClubQuery, PaginatedList<MatchByClubDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchesByClubQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PaginatedList<MatchByClubDto>> Handle(GetMatchesByClubQuery request, CancellationToken cancellationToken)
        {
            PaginatedList<MatchByClubDto> matches;

            matches = await context.Matches
                    .Where(x => request.IsFutureMatches.GetUpcomingOrNot(x.KickOff))
                    .Where(x => x.HomeTeam.Id == request.ClubId || x.AwayTeam.Id == request.ClubId)
                    .OrderBy(x => x.KickOff)
                    .ProjectTo<MatchByClubDto>(mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);

            matches.Items.ForEach(x => x.IsHome = request.ClubId == x.HomeTeam.Id);

            return matches;
        }
    }
}
