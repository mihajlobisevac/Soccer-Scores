using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages
{
    public class MatchModel : PageModel
    {
        public int MatchId { get; set; }

        public void OnGet(int id)
        {
            MatchId = id;
        }
    }
}
