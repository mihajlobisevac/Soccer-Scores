using AutoMapper;
using MediatR;
using SoccerScores.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Matches.Commands.Incidents.CreateIncident
{
    public class CreateIncidentCommand : IRequest<int>
    {
        public IncidentDto Incident { get; set; }
    }

    public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public CreateIncidentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
        {
            var entity = await HandleIncidentExceptions.Handle(request.Incident, context, mapper);

            context.Incidents.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
