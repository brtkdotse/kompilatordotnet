using Kompilator.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kompilator.Pages
{
    public class EpisodePage : PageModel
    {
        private readonly ILogger<GenericPage> _logger;
        private readonly SimplecastService _service;

        [BindProperty(SupportsGet = true)]
        public string EpisodeId { get; set; }

        public Episode Episode { get; set; }
        public EpisodePage(ILogger<GenericPage> logger, SimplecastService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task OnGet()
        {
            string showId = "b7258c05-be18-4f6b-af75-fb9639220d9d";

            Episode = await _service.GetEpisodeAsync(EpisodeId, showId);
        }
    }
}