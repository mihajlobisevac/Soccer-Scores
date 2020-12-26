using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Matches;
using System;
using System.Collections.Generic;

namespace Scores.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetFixtures.Response> Fixtures { get; set; }

        public void OnGet([FromServices] GetMatchesByDate getMatches, [FromServices] GetFixtures getFixtures)
        {
            Fixtures = getFixtures.Do(DateTime.Now);
        }
    }
}
