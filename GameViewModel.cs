using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GroupNineMobileProject
{
    public class GamesViewModel
    {
        private readonly IgdbService _igdbService;

        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();

        public GamesViewModel()
        {
            _igdbService = new IgdbService();
        }



        public async Task LoadAllGamesAsync() // This method can be renamed if needed, but it should call GetGamesAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Trying LoadAllgamesAsync"); // Log API error

                Games.Clear();
                var games = await _igdbService.GetGamesAsync(""); // Use GetGamesAsync here instead

                foreach (var game in games)
                {
                    Games.Add(game); // Add the fetched games to the ObservableCollection
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading games: {ex.Message}");
            }
        }

        //Grabs random game for home page
        public async Task<Game?> GetRandomGameAsync()
        {
            try
            {
                var games = await _igdbService.GetGamesAsync("any");

                if (games?.Count > 0)
                {
                    var random = new Random();
                    return games[random.Next(games.Count)];
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting random game: {ex.Message}");
            }

            return null;
        }
    }

}
