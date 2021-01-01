using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Matches;

namespace Scores.UI.Pages
{
    public class MatchModel : PageModel
    {
        public GetMatchById.Response Match { get; set; }

        public void OnGet([FromServices] GetMatchById getMatch, int id)
        {
            Match = getMatch.Do(id);
        }
    }
}
