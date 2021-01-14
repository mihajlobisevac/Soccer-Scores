using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Matches;
using Scores.Application.Guest.Players;
using Scores.Application.Guest.Standings;

namespace Scores.UI.Pages
{
    public class PlayerModel : PageModel
    {
        public GetPlayerById.Response Player { get; set; }
        public IEnumerable<GetMatchById.Response> Matches { get; set; }

        public void OnGet(
            [FromServices] GetPlayerById getPlayer, 
            [FromServices] GetMatchesByPlayerId getPlayerMatches, 
            [FromServices] GetMatchById getMatch,
            [FromServices] GetClubById getClub,
            [FromServices] GetStandingsById getStandings,
            [FromServices] GetEventsByMatchId getEvents,
            int id)
        {
            Player = getPlayer.Do(id);
            Matches = getPlayerMatches.Do(id, getMatch, getClub, getStandings, getEvents, getPlayer);
        }
    }
}
