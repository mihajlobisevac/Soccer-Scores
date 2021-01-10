using Scores.Application.Guest.Tournaments;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Standings
{
    [Service]
    public class GetStandingsById
    {
        private readonly IStandingsManager standingsManager;
        private readonly ITournamentManager tournamentManager;
        private readonly ICountryManager countryManager;

        public GetStandingsById(IStandingsManager standingsManager, ITournamentManager tournamentManager, ICountryManager countryManager)
        {
            this.standingsManager = standingsManager;
            this.tournamentManager = tournamentManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public GetTournamentById.Response Tournament { get; set; }
        }

        public Response Do(int id)
        {
            var getTournament = new GetTournamentById(tournamentManager, countryManager, standingsManager);

            return standingsManager.GetStandingsById(id, x => new Response
            {
                Id = x.Id,
                Tournament = getTournament.Do(x.TournamentId),
            });
        }
    }
}
