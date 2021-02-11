using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages.Admin.Countries
{
    public class EditModel : PageModel
    {
        public int CountryId { get; set; }

        public void OnGet(int id)
        {
            CountryId = id;
        }
    }
}
