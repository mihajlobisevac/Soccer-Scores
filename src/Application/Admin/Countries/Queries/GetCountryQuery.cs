using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Countries.Queries
{ 
    public class GetCountryQuery : IRequest<CountryDto>
    {
        public int Id { get; set; }
    }

    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, CountryDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCountryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CountryDto> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Countries
                .Include(x => x.Cities)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<CountryDto>(entity);
        }
    }
}
