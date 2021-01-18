using Scores.Domain.Infrastructure;
using Scores.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Scores.Application.TournamentsAdmin
{
    [Service]
    public class CreateTournament
    {
        private readonly ITournamentManager tournamentManager;

        public CreateTournament(ITournamentManager tournamentManager)
        {
            this.tournamentManager = tournamentManager;
        }
        
        public class Request
        {
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
            var tournament = new Tournament
            {
                Name = request.Name,
                HasGroupStage = request.HasGroupStage,
                CountryId = request.CountryId
            };

            var result = await tournamentManager.CreateTournament(tournament);

            if (result <= 0)
                throw new Exception("Failed to create tournament");

            return new Response
            {
                Id = tournament.Id,
                Name = tournament.Name,
                HasGroupStage = tournament.HasGroupStage,
                CountryId = tournament.CountryId
            };
        }
    }
}
