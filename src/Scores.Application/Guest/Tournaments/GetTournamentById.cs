using Scores.Application.Guest.Countries;
using Scores.Application.Guest.Standings;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scores.Application.Guest.Tournaments
{
    [Service]
    public class GetTournamentById
    {
        private readonly ITournamentManager tournamentManager;
        private readonly ICountryManager countryManager;
        private readonly IStandingsManager standingsManager;

        public GetTournamentById(ITournamentManager tournamentManager, ICountryManager countryManager,
            IStandingsManager standingsManager)
        {
            this.tournamentManager = tournamentManager;
            this.countryManager = countryManager;
            this.standingsManager = standingsManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool HasGroupStage { get; set; }
            public GetCountryById.Response Country { get; set; }
            public int CurrentStandingsId { get; set; }
        }

        public Response Do(int id)
        {
            var getStandings = new GetStandingsByTournamentId(standingsManager);

            return tournamentManager.GetTournamentById(id, x => new Response
            {
                Id = x.Id,
                Name = x.Name,
                HasGroupStage = x.HasGroupStage,
                Country = countryManager.GetCountryById(x.CountryId,
                    y => new GetCountryById.Response
                    {
                        Id = y.Id,
                        Name = y.Name,
                        NameCode = y.NameCode,
                        Flag = y.Flag,
                    }),
                CurrentStandingsId = getStandings.Do(x.Id).Id,
            });
        }
    }
}
