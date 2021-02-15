using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages.Admin.Clubs
{
    public class EditModel : PageModel
    {
        public int ClubId { get; set; }
        public void OnGet(int id)
        {
            ClubId = id;
        }
    }
}
