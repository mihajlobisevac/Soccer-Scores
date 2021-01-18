using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Competitions.Queries
{
    public class GetCompetitionsQuery : IRequest<CompetitionsVm>
    {
    }

    public class GetCompetitionsQueryHandler : IRequestHandler<GetCompetitionsQuery, CompetitionsVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCompetitionsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CompetitionsVm> Handle(GetCompetitionsQuery request, CancellationToken cancellationToken)
        {
            return new CompetitionsVm
            {
                Competitions = await context.Competitions
                    .Include(x => x.Country)
                    .OrderBy(x => x.Country)
                    .ProjectTo<CompetitionDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
