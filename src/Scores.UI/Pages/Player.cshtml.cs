using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Players;

namespace Scores.UI.Pages
{
    public class PlayerModel : PageModel
    {
        public GetPlayerById.Response Player { get; set; }
        public void OnGet([FromServices] GetPlayerById getPlayer, int id)
        {
            Player = getPlayer.Do(id);
        }
    }
}
