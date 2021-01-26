using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Matches.Queries.GetMatchesBySeason.Models;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Queries.GetMatchesBySeason
{
    public class GetMatchesBySeasonQuery : IRequest<SeasonWithMatchesVm>
    {
        public int SeasonId { get; set; }
        public int? SpecifiedGameWeek { get; set; }

        internal bool HasSpecifiedGameWeek => SpecifiedGameWeek != null;
    }

    public class GetMatchesBySeasonQueryHandler : IRequestHandler<GetMatchesBySeasonQuery, SeasonWithMatchesVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchesBySeasonQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SeasonWithMatchesVm> Handle(GetMatchesBySeasonQuery request, CancellationToken cancellationToken)
        {
            var season = mapper.Map<SeasonWithMatchesVm>(await GetSeason(request.SeasonId));

            if (request.HasSpecifiedGameWeek)
            {
                season.Matches = await GetMatchesByGameWeek(request);
                season.GameWeek = (int)request.SpecifiedGameWeek;

                return season;
            }

            season.Matches = await GetLatestMatches(request);
            season.GameWeek = LatestGameWeek(request);

            return season;
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
