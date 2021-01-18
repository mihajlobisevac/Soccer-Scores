﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Seasons.Queries
{
    public class GetSeasonsByCompetitionQuery : IRequest<SeasonsVm>
    {
        public int CompetitionId { get; set; }
    }

    public class GetSeasonsByCompetitionQueryHandler : IRequestHandler<GetSeasonsByCompetitionQuery, SeasonsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetSeasonsByCompetitionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SeasonsVm> Handle(GetSeasonsByCompetitionQuery request, CancellationToken cancellationToken)
        {
            return new SeasonsVm
            {
                Seasons = await context.Seasons
                    .Where(x => x.Competition.Id == request.CompetitionId)
                    .Include(x => x.Competition)
                    .OrderByDescending(x => x.End)
                    .ProjectTo<SeasonDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
