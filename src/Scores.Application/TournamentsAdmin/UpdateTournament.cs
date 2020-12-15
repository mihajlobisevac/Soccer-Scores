using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.TournamentsAdmin
{
    [Service]
    public class UpdateTournament
    {
        private readonly ITournamentManager tournamentManager;

        public UpdateTournament(ITournamentManager tournamentManager)
        {
            this.tournamentManager = tournamentManager;
        }

        public class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool HasGroupStage { get; set; }
            public int CountryId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool HasGroupStage { get; set; }
            public int CountryId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var city = tournamentManager.GetTournamentById(request.Id, x => x);

            city.Name = request.Name;
            city.HasGroupStage = request.HasGroupStage;
            city.CountryId = request.CountryId;

            await tournamentManager.UpdateTournament(city);

            return new Response
            {
                Id = city.Id,
                Name = city.Name,
                HasGroupStage = city.HasGroupStage,
                CountryId = city.CountryId,
            };
        }
    }
}
