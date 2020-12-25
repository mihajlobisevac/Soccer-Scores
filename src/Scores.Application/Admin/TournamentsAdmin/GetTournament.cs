using Scores.Domain.Infrastructure;

namespace Scores.Application.TournamentsAdmin
{
    [Service]
    public class GetTournament
    {
        private readonly ITournamentManager tournamentManager;

        public GetTournament(ITournamentManager tournamentManager)
        {
            this.tournamentManager = tournamentManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool HasGroupStage { get; set; }
            public bool Deactivated { get; set; }
            public int CountryId { get; set; }
        }

        public Response Do(int id) =>
            tournamentManager.GetTournamentById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                HasGroupStage = x.HasGroupStage,
                Deactivated = x.Deactivated,
                CountryId = x.CountryId,
            });
    }
}
