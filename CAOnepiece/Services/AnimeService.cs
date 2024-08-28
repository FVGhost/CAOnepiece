using AnimeInfoApp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnimeInfoApp.Services
{
    public class AnimeService
    {
        private readonly HttpClient _httpClient;

        public AnimeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Anime> GetAnimeAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/anime/{id}/full");
            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();
            using var jsonDocument = await JsonDocument.ParseAsync(responseStream);

            var animeJson = jsonDocument.RootElement.GetProperty("data");

            var anime = new Anime
            {
                MalId = animeJson.GetProperty("mal_id").GetInt32(),
                Title = animeJson.GetProperty("title").GetString(),
                Synopsis = animeJson.GetProperty("synopsis").GetString(),
                ImageUrl = animeJson.GetProperty("images").GetProperty("jpg").GetProperty("image_url").GetString(),
                AiredFrom = animeJson.GetProperty("aired").GetProperty("from").TryGetDateTime(out var airedFrom) ? airedFrom : (DateTime?)null,
                AiredTo = animeJson.GetProperty("aired").TryGetProperty("to", out var airedTo) && airedTo.TryGetDateTime(out var airedToDate) ? airedToDate : (DateTime?)null,
                Type = animeJson.GetProperty("type").GetString(),
                Episodes = animeJson.GetProperty("episodes").GetInt32(),
                Status = animeJson.GetProperty("status").GetString(),
                Score = animeJson.GetProperty("score").GetDouble(),
                Members = animeJson.GetProperty("members").GetInt32(),
                Url = animeJson.GetProperty("url").GetString()
            };

            return anime;
        }
    }
}
