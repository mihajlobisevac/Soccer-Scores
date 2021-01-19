using AutoMapper;
using MediatR;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Clubs.Queries.GetClubQuery
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
            var entity = await context.Clubs.FindAsync(request.Id);

            return mapper.Map<ClubDto>(entity);
        }
    }
}
