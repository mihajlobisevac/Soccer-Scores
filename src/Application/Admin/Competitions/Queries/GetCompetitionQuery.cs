using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Competitions.Queries
{
    public class GetCompetitionQuery : IRequest<CompetitionDto>
    {
        public int Id { get; set; }
    }

    public class GetCompetitionQueryHandler : IRequestHandler<GetCompetitionQuery, CompetitionDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCompetitionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CompetitionDto> Handle(GetCompetitionQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Competitions
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<CompetitionDto>(entity);
        }
    }
}
