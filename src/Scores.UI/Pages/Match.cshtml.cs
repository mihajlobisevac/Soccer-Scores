using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Matches;
using Scores.Application.Guest.Players;

namespace Scores.UI.Pages
{
    public class MatchModel : PageModel
    {
        public GetMatchById.Response Match { get; set; }
        public List<GetPlayersByMatchId.Response> Lineups { get; set; }

        public void OnGet([FromServices] GetMatchById getMatch, [FromServices] GetPlayersByMatchId getLineups, int id)
        {
            Match = getMatch.Do(id);
            Lineups = getLineups.Do(id).ToList();
        }
    }
}
