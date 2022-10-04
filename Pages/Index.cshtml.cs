using Kompilator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kompilator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SimplecastService _simplecastService;
        public Episode LatestEpisode => Episodes.OrderByDescending(x => x.EpisodeNumber).First();
        public IndexModel(ILogger<IndexModel> logger, SimplecastService simplecastService)
        {
            _logger = logger;
            _simplecastService = simplecastService;
        }

        public List<Episode> Episodes { get; private set; }

        public async Task OnGet()
        {
            string id = "b7258c05-be18-4f6b-af75-fb9639220d9d";
            var episodes = await _simplecastService.GetAllEpisodesAsync(id);
            Episodes = episodes.Where(x => x.Published != null).ToList();
        }
    }
}