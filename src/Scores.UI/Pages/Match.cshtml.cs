using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Matches;
using Scores.Application.Guest.Players;
using Scores.Application.Guest.Standings;

namespace Scores.UI.Pages
{
    public class MatchModel : PageModel
    {
        public GetMatchById.Response Match { get; set; }
        public List<GetPlayersByMatchId.Response> Lineups { get; set; }

        public void OnGet(
            [FromServices] GetMatchById getMatch, 
            [FromServices] GetPlayersByMatchId getLineups, 
            [FromServices] GetClubById getClub, 
            [FromServices] GetStandingsById getStandings, 
            [FromServices] GetEventsByMatchId getEvents,
            [FromServices] GetPlayerById getPlayer,
            int id)
        {
            Match = getMatch.Do(id, getClub, getStandings, getEvents, getPlayer);
            Lineups = getLineups.Do(id).ToList();
        }
    }
}
