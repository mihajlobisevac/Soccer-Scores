using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace SoccerScores.WebUI.Pages.Admin.Players
{
    public class EditModel : PageModel
    {
        public int PlayerId { get; set; }
        public IEnumerable<string> Positions { get; set; } = Enum.GetNames(typeof(Position));
        public IEnumerable<string> Foots { get; set; } = Enum.GetNames(typeof(Foot));

        public void OnGet(int id)
        {
            PlayerId = id;
        }
    }
}
