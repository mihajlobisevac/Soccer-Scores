using Application.Common.Extensions;
using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Competitions.Commands.CreateCompetition
{
    public class CreateCompetitionCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int CountryId { get; set; }
    }

    public class CreateCompetitionCommandHandler : IRequestHandler<CreateCompetitionCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateCompetitionCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Competition
            {
                Name = request.Name,
                Type = request.Type.ToEnum<CompetitionType>(),
                Country = await GetCountry(request.CountryId)
            };

            context.Competitions.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        private async Task<Country> GetCountry(int id)
        {
            var country = await context.Countries.FindAsync(id);

            if (country is null)
            {
                throw new NotFoundException(nameof(Country), id);
            }

            return country;
        }
    }
}
