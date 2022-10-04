using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

namespace Kompilator.Services
{
    public class SimplecastService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string allEpisodeCacheKey = "__allepisodes";
        public SimplecastService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<Episode>> GetAllEpisodesAsync(string showId)
        {
            if (_cache.TryGetValue<List<Episode>>(allEpisodeCacheKey, out var cachedEpisodes))
                return cachedEpisodes;

            var result = await _httpClient.GetAsync($"podcasts/{showId}/episodes?limit=200");
            result.EnsureSuccessStatusCode();
            var episodes = await result.Content.ReadFromJsonAsync<SimplecastEpisodesResponse>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            _cache.Set(allEpisodeCacheKey, episodes.Collection, new DateTimeOffset(DateTime.Now.AddHours(1)));

            return episodes.Collection;
        }

        public async Task<Episode?> GetEpisodeAsync(string episodeId, string showId)
        {
            var allEpisodes = await GetAllEpisodesAsync(showId);

            var episode = allEpisodes.Where(x => x.EpisodeNumber == Convert.ToInt32(episodeId)).FirstOrDefault();
       
            var result = await _httpClient.GetAsync($"episodes/{episode.Id}");
            result.EnsureSuccessStatusCode();
            var fetchedEpisode = await result.Content.ReadFromJsonAsync<Episode>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return fetchedEpisode;
        }
    }

    public class SimplecastSingleEpisodeResponse
    {

    }

    public class SimplecastEpisodesResponse
    {
        [JsonPropertyName("collection")]
        public List<Episode> Collection { get; set; }
    }

    public class Episode
    {
        [JsonPropertyName("published_at")]
        public DateTime Published { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        public string Title { get; set; }

        [JsonPropertyName("number")]
        public int EpisodeNumber { get; set; }

        [JsonPropertyName("long_description")]
        public string LongDescription { get; set; }
    }
}
