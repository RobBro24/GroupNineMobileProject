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
        private readonly string _authToken = "x7a4x9x1lds5u4hlvwoxue2l5cp4f2"; // Auth token for IGDB API

        public IgdbService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.igdb.com/v4/")
            };

            _httpClient.DefaultRequestHeaders.Add("Client-ID", _clientId);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
        }

        public async Task<List<Game>> GetGamesAsync(string searchQuery)
        {
            System.Diagnostics.Debug.WriteLine("Starting API Call...");

            // Construct the query for fetching games sorted by rating in descending order
            var query = @"fields name, summary, involved_companies.company.name, involved_companies.publisher; sort rating desc; limit 50;";
            var content = new StringContent(query, Encoding.UTF8, "text/plain");

            System.Diagnostics.Debug.WriteLine("Sending request to IGDB API...");

            // Make the POST request to the IGDB API
            var response = await _httpClient.PostAsync("games", content);

            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"API Error: {response.StatusCode}");
                throw new Exception($"IGDB API Error: {response.StatusCode}");
            }

            // Parse the response into a list of Game objects
            var json = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("Received response from IGDB API...");

            // Deserialize the JSON response into a List of Game objects
            return JsonSerializer.Deserialize<List<Game>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Game>();
        }
    }
}
