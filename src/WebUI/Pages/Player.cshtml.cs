using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages
{
    public class PlayerModel : PageModel
    {
        public string Player { get; set; }
        public IEnumerable<string> Matches { get; set; } = new List<string>();

        public void OnGet(int id)
        {
        }
    }
}
