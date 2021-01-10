using Scores.Application.Guest.Countries;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scores.Application.Guest.Tournaments
{
    [Service]
    public class GetTournaments
    {
        private readonly ITournamentManager tournamentManager;
        private readonly ICountryManager countryManager;

        public GetTournaments(ITournamentManager tournamentManager, ICountryManager countryManager)
        {
            this.tournamentManager = tournamentManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public string CountryName { get; set; }
            public List<GetTournamentById.Response> Tournaments { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            List<Response> Countries = new List<Response>();

            var tournaments = GetTourneys();
            var countryId = -1;
            var countryIndex = -1;

            for (int i = 0; i < tournaments.Count; i++)
            {
                if (countryId != tournaments[i].Country.Id)
                {
                    Countries.Add(new Response 
                        {
                            CountryName = tournaments[i].Country.Name,
                            Tournaments = new List<GetTournamentById.Response>() 
                        });

                    countryIndex++;
                }

                Countries[countryIndex]
                    .Tournaments
                    .Add(tournaments[i]);

                countryId = tournaments[i].Country.Id;
            }

            return Countries;
        }

        private List<GetTournamentById.Response> GetTourneys()
            => tournamentManager
                .GetTournaments(x => 
                    new GetTournamentById.Response
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
                    })
                .OrderBy(x => x.Country.Id)
                .ToList();
    }
}
