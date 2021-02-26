using AutoMapper;
using MediatR;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Countries.Queries.GetCountry
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
            var country = await context.Countries.FindAsync(request.Id);

            return mapper.Map<CountryDto>(country);
        }
    }
}
