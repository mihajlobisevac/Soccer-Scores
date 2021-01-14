using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Standings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetFixturesByDate
    {
        public class Response
        {
            public GetStandingsById.Response Standings { get; set; }
            public List<MatchViewModel> Matches { get; set; }

            public Response(GetStandingsById.Response standings)
            {
                Standings = standings;
                Matches = new List<MatchViewModel>();
            }
        }

        public class MatchViewModel
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public GetClubById.Response HomeTeam { get; set; }
            public GetClubById.Response AwayTeam { get; set; }
            public GetStandingsById.Response Standings { get; set; }
            public IEnumerable<GetEventsByMatchId.Response> Incidents { get; set; }
        }

        public IEnumerable<Response> Do(
            DateTime date, 
            GetMatchesByDate getMatches,
            GetClubById getClub,
            GetStandingsById getStandings,
            GetEventsByMatchId getEvents)
        {
            var tournaments = GetTournamentsWithMatches(date, getMatches, getClub, getStandings, getEvents);

            return CreateFixtures(tournaments, getStandings);
        }

        private List<List<MatchViewModel>> GetTournamentsWithMatches(
            DateTime date, 
            GetMatchesByDate getMatches,
            GetClubById getClub,
            GetStandingsById getStandings,
            GetEventsByMatchId getEvents)
        {
            var matches = GetMatchesByDate(date, getMatches, getClub, getStandings, getEvents);
            var tournaments = ExtractTournamentsFromMatches(matches);

            return tournaments;
        }

        private IEnumerable<MatchViewModel> GetMatchesByDate(
            DateTime date, 
            GetMatchesByDate getMatches,
            GetClubById getClub,
            GetStandingsById getStandings,
            GetEventsByMatchId getEvents)
        {
            return getMatches.Do(date, getClub, getStandings, getEvents)
                .Select(x => new MatchViewModel
                {
                    Id = x.Id,
                    KickOff = x.KickOff,
                    HomeTeam = x.HomeTeam,
                    AwayTeam = x.AwayTeam,
                    Standings = x.Standings,
                    Incidents = x.Incidents,
                });
        }

        private List<List<MatchViewModel>> ExtractTournamentsFromMatches(IEnumerable<MatchViewModel> matches)
        {
            var extractedTournaments = matches
                .GroupBy(item => item.Standings.Id)
                .Select(group => group.ToList())
                .ToList();

            return extractedTournaments;
        }

        private List<Response> CreateFixtures(List<List<MatchViewModel>> tournaments, GetStandingsById getStandings)
        {
            List<Response> Fixtures = new List<Response>();

            int tournamentId = -1;

            for (int i = 0; i < tournaments.Count; i++)
            {
                foreach (var match in tournaments[i])
                {
                    if (tournamentId != match.Standings.Tournament.Id)
                    {
                        tournamentId = match.Standings.Tournament.Id;

                        Fixtures.Add(new Response(getStandings.Do(match.Standings.Id)));
                    }

                    Fixtures[i].Matches.Add(match);
                }
            }

            return Fixtures;
        }
    }
}
