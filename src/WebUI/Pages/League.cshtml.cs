using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages
{
    public class LeagueModel : PageModel
    {
        public int SeasonId { get; set; }
        public string Standings { get; set; }
        public IEnumerable<string> Matches { get; set; } = new List<string>();
        public IEnumerable<string> Table { get; set; } = new List<string>();

        public void OnGet(int id)
        {
            SeasonId = id;
        }
    }
}
