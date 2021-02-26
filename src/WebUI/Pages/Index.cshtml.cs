using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SoccerScores.Application.Fixtures.Queries;

namespace SoccerScores.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

        public IEnumerable<SeasonWithMatches> Fixtures { get; set; } = new List<SeasonWithMatches>();

        public async Task OnGetAsync()
        {
            Fixtures = await Mediator.Send(new GetFixturesQuery());
        }
    }
}
