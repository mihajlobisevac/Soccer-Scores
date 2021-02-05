using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace SoccerScores.WebUI.Pages
{
    public class ClubModel : PageModel
    {
        public string Club { get; set; }
        public List<string> Matches { get; set; } = new List<string>();
        public List<string> Squad { get; set; } = new List<string>();

        public void OnGet(int id)
        {
        }
    }
}
