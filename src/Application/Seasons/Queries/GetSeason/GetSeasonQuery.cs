using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Seasons.Queries.GetSeason
{
    public class GetSeasonQuery : IRequest<SeasonDto>
    {
        public int Id { get; set; }
    }

    public class GetSeasonQueryHandler : IRequestHandler<GetSeasonQuery, SeasonDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetSeasonQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<SeasonDto> Handle(GetSeasonQuery request, CancellationToken cancellationToken)
        {
            var season = await context.Seasons
                .Include(x => x.Competition).ThenInclude(y => y.Country)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<SeasonDto>(season);
        }
    }
}
