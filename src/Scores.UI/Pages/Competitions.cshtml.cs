using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Tournaments;

namespace Scores.UI.Pages
{
    public class CompetitionsModel : PageModel
    {
        public IEnumerable<GetTournaments.Response> Countries { get; set; }

        public void OnGet([FromServices] GetTournaments getTournaments)
        {
            Countries = getTournaments.Do();
        }
    }
}
