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

        public void OnGet([FromServices] GetFixtures getFixtures)
        {
            Fixtures = getFixtures.DoByDate(new DateTime(2020, 12, 26)); // DateTime.Now default // this is for dummy data
        }
    }
}
