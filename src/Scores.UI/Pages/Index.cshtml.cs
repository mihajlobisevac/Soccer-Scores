using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Matches;
using Scores.Application.Guest.Players;
using Scores.Application.Guest.Standings;
using System;
using System.Collections.Generic;

namespace Scores.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetFixturesByDate.Response> Fixtures { get; set; }

        public void OnGet(
            [FromServices] GetFixturesByDate getFixtures,
            [FromServices] GetMatchesByDate getMatches,
            [FromServices] GetClubById getClub,
            [FromServices] GetStandingsById getStandings,
            [FromServices] GetEventsByMatchId getEvents,
            [FromServices] GetPlayerById getPlayer)
        {
            Fixtures = getFixtures
                .Do(new DateTime(2020, 12, 26), getMatches, getClub, getStandings, getEvents, getPlayer);
        }
    }
}
