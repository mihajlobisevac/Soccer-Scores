using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Matches;
using System.Collections.Generic;
using System.Linq;

namespace Scores.UI.Pages
{
    public class IndexModel : PageModel
    {
        public List<List<GetMatchesByDate.Response>> Fixtures { get; set; }

        public void OnGet([FromServices] GetMatchesByDate getMatches)
        {
            Fixtures = getMatches.Do()
                .GroupBy(item => item.Standings.TournamentId)
                .Select(group => group.ToList())
                .ToList();
        }
    }
}
