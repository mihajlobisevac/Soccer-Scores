using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Scores.UI.Pages
{
    public class MatchModel : PageModel
    {
        public string Id { get; set; }
        public void OnGet(string id)
        {
            Id = id;
        }
    }
}