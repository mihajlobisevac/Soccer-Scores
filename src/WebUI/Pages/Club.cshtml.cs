using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace SoccerScores.WebUI.Pages
{
    public class ClubModel : PageModel
    {
        public int ClubId { get; set; }

        public void OnGet(int id)
        {
            ClubId = id;
        }
    }
}
