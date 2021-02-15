using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Clubs.Queries.GetClubsByCountry
{
    public class GetClubsByCountryQuery : IRequest<IEnumerable<ClubDto>>
    {
        public int CountryId { get; set; }
    }

    public class GetClubsByCountryQueryHandler : IRequestHandler<GetClubsByCountryQuery, IEnumerable<ClubDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetClubsByCountryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ClubDto>> Handle(GetClubsByCountryQuery request, CancellationToken cancellationToken)
        {
            return await context.Clubs
                .Where(x => x.City.Country.Id == request.CountryId)
                .ProjectTo<ClubDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
