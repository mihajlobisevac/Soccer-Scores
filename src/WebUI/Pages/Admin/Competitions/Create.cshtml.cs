using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace SoccerScores.WebUI.Pages.Admin.Competitions
{
    public class CreateModel : PageModel
    {
        public IEnumerable<string> CompetitionTypes { get; set; } = Enum.GetNames(typeof(CompetitionType));

        public void OnGet()
        {
        }
    }
}
