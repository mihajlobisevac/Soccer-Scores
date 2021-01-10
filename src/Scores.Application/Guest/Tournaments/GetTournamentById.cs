using Scores.Application.Guest.Countries;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Tournaments
{
    [Service]
    public class GetTournamentById
    {
        private readonly ITournamentManager tournamentManager;
        private readonly ICountryManager countryManager;

        public GetTournamentById(ITournamentManager tournamentManager, ICountryManager countryManager)
        {
            this.tournamentManager = tournamentManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public string Name { get; set; }
            public bool HasGroupStage { get; set; }
            public GetCountryById.Response Country { get; set; }
        }

        public Response Do(int id) =>
            tournamentManager.GetTournamentById(id, x => new Response
            {
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
            });
    }
}
