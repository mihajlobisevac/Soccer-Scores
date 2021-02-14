using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages.Admin.Competitions
{
    public class EditModel : PageModel
    {
        public int CompetitionId { get; set; }

        public void OnGet(int id)
        {
            CompetitionId = id;
        }
    }
}
