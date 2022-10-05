using Kompilator.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;

namespace Kompilator.Pages
{
    public class EpisodePage : PageModel
    {
        private readonly SimplecastService _service;
        private readonly string _showId;

        [BindProperty(SupportsGet = true)]
        public string EpisodeId { get; set; }

        public Episode Episode { get; set; }
        public EpisodePage(SimplecastService service, IConfiguration configuration)
        {
            _service = service;
            _showId = configuration.GetValue<string>("ShowId");

        }

        public async Task OnGet()
        {
            Episode = await _service.GetEpisodeAsync(EpisodeId, _showId);
        }
    }
}