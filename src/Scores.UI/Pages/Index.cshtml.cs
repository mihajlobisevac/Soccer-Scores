using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scores.Application.Guest.Matches;
using System;
using System.Collections.Generic;

namespace Scores.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetFixturesByDate.Response> Fixtures { get; set; }

        public void OnGet([FromServices] GetFixturesByDate getFixtures)
        {
            Fixtures = getFixtures.Do(new DateTime(2020, 12, 26)); // DateTime.Now default // this is for dummy data
        }
    }
}
