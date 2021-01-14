using Scores.Application.Guest.Countries;
using Scores.Application.Guest.Standings;
using Scores.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Scores.Application.Guest.Tournaments
{
    [Service]
    public class GetTournaments
    {
        private readonly ITournamentManager tournamentManager;
        private readonly ICountryManager countryManager;
        private readonly IStandingsManager standingsManager;

        public GetTournaments(ITournamentManager tournamentManager, ICountryManager countryManager,
            IStandingsManager standingsManager)
        {
            this.tournamentManager = tournamentManager;
            this.countryManager = countryManager;
            this.standingsManager = standingsManager;
        }

        public class Response
        {
            public string CountryName { get; set; }
            public string CountryFlag { get; set; }
            public List<GetTournamentById.Response> Tournaments { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            var tournaments = GetTourneys();

            return GroupTournamentsByCountry(tournaments);
        }

        private static IEnumerable<Response> GroupTournamentsByCountry(List<GetTournamentById.Response> tournaments)
        {
            List<Response> Countries = new List<Response>();
            var countryId = -1;
            var countryIndex = -1;

            for (int i = 0; i < tournaments.Count; i++)
            {
                if (countryId != tournaments[i].Country.Id)
                {
                    Countries.Add(new Response
                    {
                        CountryName = tournaments[i].Country.Name,
                        CountryFlag = tournaments[i].Country.Flag,
                        Tournaments = new List<GetTournamentById.Response>()
                    });

                    countryIndex++;
                }

                Countries[countryIndex].Tournaments.Add(tournaments[i]);

                countryId = tournaments[i].Country.Id;
            }

            return Countries;
        }

        private List<GetTournamentById.Response> GetTourneys()
        {
            var getStandings = new GetStandingsByTournamentId(standingsManager);
            var getCountry = new GetCountryById(countryManager);

            return tournamentManager
                .GetTournaments(t => new GetTournamentById.Response
                    {
                        Name = t.Name,
                        HasGroupStage = t.HasGroupStage,
                        Country = getCountry.Do(t.CountryId),
                        CurrentStandingsId = getStandings.Do(t.Id).Id,
                    })
                .OrderBy(x => x.Country.Id)
                .ToList();
        }
    }
}
