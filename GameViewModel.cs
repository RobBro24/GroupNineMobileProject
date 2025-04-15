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

        public async Task LoadGamesAsync(string searchQuery)
        {
            try
            {
                Games.Clear();
                var games = await _igdbService.GetGamesAsync(searchQuery);

                foreach (var game in games)
                {
                    Games.Add(game);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading games: {ex.Message}");
            }
        }

    }
}
