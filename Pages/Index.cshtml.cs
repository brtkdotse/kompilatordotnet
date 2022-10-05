using Kompilator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kompilator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SimplecastService _simplecastService;
        private readonly string _showId;

        public Episode LatestEpisode => Episodes.OrderByDescending(x => x.EpisodeNumber).First();

        public IndexModel(SimplecastService simplecastService, IConfiguration configuration)
        {
            _simplecastService = simplecastService;
            _showId = configuration.GetValue<string>("ShowId");
        }

        public List<Episode> Episodes { get; private set; }

        public async Task OnGet()
        {
            var episodes = await _simplecastService.GetAllEpisodesAsync(_showId);
            Episodes = episodes.Where(x => x.Published != null).ToList();
        }
    }
}