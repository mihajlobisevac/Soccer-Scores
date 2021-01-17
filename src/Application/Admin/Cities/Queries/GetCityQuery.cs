using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Cities.Queries
{
    public class GetCityQuery : IRequest<CityDto>
    {
        public int Id { get; set; }
    }

    public class GetCityQueryHandler : IRequestHandler<GetCityQuery, CityDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCityQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CityDto> Handle(GetCityQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Cities
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<CityDto>(entity);
        }
    }
    }
