using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages.Admin.Matches
{
    public class EditModel : PageModel
    {
        public int MatchId { get; set; }

        public void OnGet(int id)
        {
            MatchId = id;
        }
    }
}
