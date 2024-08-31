using AnimeInfoApp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AnimeInfoApp.Services;

namespace AnimeInfoApp.Services
{
    public class AnimeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AnimeService> _logger;

        public AnimeService(HttpClient httpClient, ILogger<AnimeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Anime> GetAnimeAsync(int id)
        {
            var requestUrl = $"https://api.jikan.moe/v4/anime/{id}/full";
            _logger.LogInformation("Requesting data from URL: {RequestUrl}", requestUrl);

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(requestUrl);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("HTTP Request Error: {ExceptionMessage}", ex.Message);
                return CreateFallbackAnime();
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("API request to {RequestUrl} failed with status code {StatusCode}", requestUrl, response.StatusCode);
                return CreateFallbackAnime();
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Received JSON Response for ID {Id}: {JsonResponse}", id, jsonResponse);

            try
            {
                using var jsonDocument = JsonDocument.Parse(jsonResponse);
                var animeJson = jsonDocument.RootElement.GetProperty("data");

                var anime = new Anime
                {
                    MalId = animeJson.GetProperty("mal_id").GetInt32(),
                    Title = GetStringProperty(animeJson, "title"),
                    Synopsis = GetStringProperty(animeJson, "synopsis"),
                    ImageUrl = GetImageUrlProperty(animeJson),
                    AiredFrom = GetDateTimeProperty(animeJson, "aired", "from"),
                    AiredTo = GetDateTimeProperty(animeJson, "aired", "to"),
                    Type = GetStringProperty(animeJson, "type"),
                   Episodes = GetNullableIntProperty(animeJson, "episodes"),
                    Status = GetStringtatusProperty(animeJson, "status"),
                    Score = GetNullableDoubleProperty(animeJson, "score"),
                    Members = (int)GetNullableIntProperty(animeJson, "members"),
                    Url = GetStringProperty(animeJson, "url")
                };

                _logger.LogInformation("Parsed Anime Data: {@Anime}", anime);
                return anime;
            }
            catch (JsonException ex)
            {
                _logger.LogError("JSON Parsing Error: {ExceptionMessage}", ex.Message);
                return CreateFallbackAnime();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Invalid Operation Error: {ExceptionMessage}", ex.Message);
                return CreateFallbackAnime();
            }
        }

        private string GetStringProperty(JsonElement jsonElement, string propertyName)
        {
            return jsonElement.TryGetProperty(propertyName, out var prop) ? prop.GetString() ?? string.Empty : string.Empty;
        }
        private string GetStringtatusProperty(JsonElement jsonElement, string propertyName)
        {
         
            if (jsonElement.TryGetProperty(propertyName, out var prop))
            {
               
                if (prop.ValueKind == JsonValueKind.String)
                {
                    return prop.GetString() ?? string.Empty;
                }
              
                else
                {
   
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        private int? GetNullableIntProperty(JsonElement jsonElement, string propertyName)
        {
         
            if (jsonElement.TryGetProperty(propertyName, out var prop))
            {
             
                switch (prop.ValueKind)
                {
                    case JsonValueKind.Number:
               
                        if (prop.TryGetInt32(out var intValue))
                        {
                            return intValue;
                        }
                        break;
                    case JsonValueKind.Null:
                      
                        return null;
                }
            }

           
            return null;
        }


        private double GetNullableDoubleProperty(JsonElement jsonElement, string propertyName)
        {
            return jsonElement.TryGetProperty(propertyName, out var prop) && prop.TryGetDouble(out var value) ? value : 0.0;
        }

        private DateTime? GetDateTimeProperty(JsonElement jsonElement, string parentPropertyName, string datePropertyName)
        {
            if (jsonElement.TryGetProperty(parentPropertyName, out var parentProp))
            {
                if (parentProp.ValueKind == JsonValueKind.Object &&
                    parentProp.TryGetProperty(datePropertyName, out var dateProp))
                {
                   
                    if (dateProp.ValueKind == JsonValueKind.String &&
                        DateTime.TryParse(dateProp.GetString(), out var dateTime))
                    {
                        return dateTime;
                    }
                    else if (dateProp.ValueKind == JsonValueKind.Null)
                    {
                 
                        return null;
                    }
                }
            }

           
            return null;
        }


        private string GetImageUrlProperty(JsonElement jsonElement)
        {
            return jsonElement.TryGetProperty("images", out var imagesJson) &&
                   imagesJson.TryGetProperty("jpg", out var jpgJson) &&
                   jpgJson.TryGetProperty("image_url", out var imageUrlJson)
                ? imageUrlJson.GetString() ?? string.Empty
                : string.Empty;
        }

        private Anime CreateFallbackAnime()
        {
            return new Anime
            {
                Title = "Unknown",
                Synopsis = "Error retrieving anime details.",
                ImageUrl = string.Empty,
                AiredFrom = null,
                AiredTo = null,
                Type = string.Empty,
                Episodes = 0,
                Status = string.Empty,
                Score = 0.0,
                Members = 0,
                Url = string.Empty
            };
        }
    }
}
