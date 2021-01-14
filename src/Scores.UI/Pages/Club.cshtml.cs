using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Matches;
using Scores.Application.Guest.Players;
using Scores.Application.Guest.Standings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scores.UI.Pages
{
    public class ClubModel : PageModel
    {
        public GetClubById.Response Club { get; set; }
        public List<GetMatchesByClubId.Response> Matches { get; set; }
        public List<GetPlayersByClubId.Response> Squad { get; set; }

        public void OnGet(
            [FromServices] GetClubById getClub, 
            [FromServices] GetMatchesByClubId getMatches, 
            [FromServices] GetPlayersByClubId getPlayers, 
            [FromServices] GetStandingsById getStandings, 
            [FromServices] GetEventsByMatchId getEvents, 
            int id)
        {
            Club = getClub.Do(id);

            Squad = getPlayers.Do(Club.Id).ToList();
            
            Matches = getMatches
                .Do(Club.Id, getClub, getStandings, getEvents)
                .ToList();

            Matches.Sort((x, y) => DateTime.Compare(x.KickOff, y.KickOff));
        }
    }
}
