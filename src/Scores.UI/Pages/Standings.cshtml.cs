using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Matches;
using Scores.Application.Guest.Standings;

namespace Scores.UI.Pages
{
    public class StandingsModel : PageModel
    {
        public GetStandingsById.Response Standings { get; set; }
        public List<GetMatchById.Response> Matches { get; set; }

        public void OnGet([FromServices] GetStandingsById getStandings, [FromServices] GetMatchesByStandingsId getMatches, int id)
        {
            Standings = getStandings.Do(id);
            Matches = getMatches.Do(id).ToList();
        }
    }
}
