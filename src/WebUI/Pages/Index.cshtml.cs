using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace SoccerScores.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<string> Fixtures { get; set; } = new List<string>();

        public void OnGet()
        {

        }
    }
}
