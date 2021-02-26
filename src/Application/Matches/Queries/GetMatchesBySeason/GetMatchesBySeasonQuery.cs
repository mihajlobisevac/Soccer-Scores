using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Queries.GetMatchesBySeason
{
    public class GetMatchesBySeasonQuery : IRequest<SeasonWithMatches>
    {
        public int SeasonId { get; set; }
        public int? SpecifiedGameWeek { get; set; }

        internal bool HasSpecifiedGameWeek => SpecifiedGameWeek is not null;
    }

    public class GetMatchesBySeasonQueryHandler : IRequestHandler<GetMatchesBySeasonQuery, SeasonWithMatches>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchesBySeasonQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SeasonWithMatches> Handle(GetMatchesBySeasonQuery request, CancellationToken cancellationToken)
        {
            var season = mapper.Map<SeasonWithMatches>(await GetSeason(request.SeasonId));

            season.Matches = await GetMatches(request);
            season.GameWeek = GetGameWeek(request);

            return season;
        }

        private int GetGameWeek(GetMatchesBySeasonQuery request)
        {
            return request.HasSpecifiedGameWeek ? (int)request.SpecifiedGameWeek : LatestGameWeek(request);
        }

        private async Task<List<MatchBySeasonDto>> GetMatches(GetMatchesBySeasonQuery request)
        {
            return request.HasSpecifiedGameWeek ? await GetMatchesByGameWeek(request) : await GetLatestMatches(request);
        }

        private async Task<List<MatchBySeasonDto>> GetMatchesByGameWeek(GetMatchesBySeasonQuery request)
            => await context.Matches
                .Where(x => x.Season.Id == request.SeasonId)
                .Where(x => x.GameWeek == request.SpecifiedGameWeek)
                .ProjectTo<MatchBySeasonDto>(mapper.ConfigurationProvider)
                .ToListAsync();

        private async Task<List<MatchBySeasonDto>> GetLatestMatches(GetMatchesBySeasonQuery request)
            => await context.Matches
                .Where(x => x.Season.Id == request.SeasonId)
                .Where(x => x.GameWeek == LatestGameWeek(request))
                .ProjectTo<MatchBySeasonDto>(mapper.ConfigurationProvider)
                .ToListAsync();

        private int LatestGameWeek(GetMatchesBySeasonQuery request)
            => context.Matches
                .Where(x => x.Season.Id == request.SeasonId)
                .Max(x => x.GameWeek);

        private async Task<Season> GetSeason(int id)
            => await context.Seasons
                .Include(x => x.Competition)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
