using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Clubs;

namespace Scores.UI.Pages
{
    public class ClubModel : PageModel
    {
        public GetClubById.Response Club { get; set; }

        public void OnGet([FromServices] GetClubById getClub, int id)
        {
            Club = getClub.Do(id);
        }
    }
}
