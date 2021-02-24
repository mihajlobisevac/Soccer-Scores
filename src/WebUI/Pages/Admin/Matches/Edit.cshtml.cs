using Domain.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace SoccerScores.WebUI.Pages.Admin.Matches
{
    public class EditModel : PageModel
    {
        public int MatchId { get; set; }
        public IEnumerable<string> IncidentTypes { get; set; } = Enum.GetNames(typeof(IncidentType));
        public IEnumerable<string> IncidentClasses { get; set; } = Enum.GetNames(typeof(IncidentClass));

        public void OnGet(int id)
        {
            MatchId = id;
        }
    }
}
