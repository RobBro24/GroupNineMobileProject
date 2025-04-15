using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GroupNineMobileProject
{
    public class IgdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId = "xcqvkq6mjlo9n4ugdly1h6suh0zngp";
        private readonly string _authToken = "5141336"; // get from Step 1

        public IgdbService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.igdb.com/v4/games")
            };

            _httpClient.DefaultRequestHeaders.Add("Client-ID", _clientId);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
        }

        public async Task<List<Game>> GetGamesAsync(string searchQuery)
        {
            var query = $@"search ""{searchQuery}""; fields name, summary, rating, involved_companies.company.name, involved_companies.publisher; limit 10;";

            var content = new StringContent(query, Encoding.UTF8, "text/plain");
            var response = await _httpClient.PostAsync("games", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"IGDB API Error: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Game>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
