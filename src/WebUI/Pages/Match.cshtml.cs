using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages
{
    public class MatchModel : PageModel
    {
        public string Match { get; set; }
        public List<string> Lineups { get; set; } = new List<string>();

        public void OnGet(int id)
        {
        }
    }
}
