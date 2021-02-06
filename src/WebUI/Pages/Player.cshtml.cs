using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoccerScores.WebUI.Pages
{
    public class PlayerModel : PageModel
    {
        public int PlayerId { get; set; }

        public void OnGet(int id)
        {
            PlayerId = id;
        }
    }
}
