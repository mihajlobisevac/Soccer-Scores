using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages
{
    public class CompetitionsModel : PageModel
    {
        public IEnumerable<string> Countries { get; set; } = new List<string>();

        public void OnGet()
        {
        }
    }
}
