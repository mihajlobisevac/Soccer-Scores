using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Matches;
using Scores.Application.Guest.Standings;

namespace Scores.UI.Pages
{
    public class StandingsModel : PageModel
    {
        public GetStandingsById.Response Standings { get; set; }
        public IEnumerable<GetMatchById.Response> Matches { get; set; }
        public IEnumerable<CreateStandingsByMatches.ClubViewModel> Table { get; set; }

        public void OnGet(
            [FromServices] GetMatchById getMatch,
            [FromServices] GetClubById getClub,
            [FromServices] GetStandingsById getStandings,
            [FromServices] GetEventsByMatchId getEvents,
            [FromServices] GetMatchesByStandingsId getMatches,
            [FromServices] CreateStandingsByMatches createStandings,
            int id)
        {
            Standings = getStandings.Do(id);

            Matches = getMatches.Do(id, getMatch, getClub, getStandings, getEvents);

            Table = createStandings.Do(Matches.ToList());
        }
    }
}
