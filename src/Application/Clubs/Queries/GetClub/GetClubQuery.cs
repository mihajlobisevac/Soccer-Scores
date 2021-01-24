using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Clubs.Queries.GetClub
{
    public class GetClubQuery : IRequest<ClubDto>
    {
        public int Id { get; set; }
    }

    public class GetClubQueryHandler : IRequestHandler<GetClubQuery, ClubDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetClubQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ClubDto> Handle(GetClubQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Clubs
                .Include(x => x.City)
                .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return mapper.Map<ClubDto>(entity);
        }
    }
}
