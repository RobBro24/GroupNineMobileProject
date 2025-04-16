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
    }

}
