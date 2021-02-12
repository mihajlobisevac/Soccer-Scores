using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages.Admin.Cities
{
    public class EditModel : PageModel
    {
        public int CityId { get; set; }

        public void OnGet(int id)
        {
            CityId = id;
        }
    }
}
