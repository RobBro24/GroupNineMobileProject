using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GroupNineMobileProject
{
    public class GamesViewModel : INotifyPropertyChanged
    {
        private readonly IgdbService _igdbService;

        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();
        public ObservableCollection<Game> FilteredGames { get; } = new ObservableCollection<Game>();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterGames();
                }
            }
        }

        public GamesViewModel()
        {
            _igdbService = new IgdbService();
        }

        public async Task LoadAllGamesAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Trying LoadAllgamesAsync");
                Games.Clear();
                FilteredGames.Clear();

                var games = await _igdbService.GetGamesAsync("");

                System.Diagnostics.Debug.WriteLine($"Games count after API call: {games.Count}");

                foreach (var game in games)
                {
                    Games.Add(game);
                    FilteredGames.Add(game); 
                }

                System.Diagnostics.Debug.WriteLine($"Total games added to FilteredGames: {FilteredGames.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading games: {ex.Message}");
            }
        }



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

        private void FilterGames()
        {
            FilteredGames.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? Games
                : new ObservableCollection<Game>(Games.Where(g => g.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));

            foreach (var game in filtered)
            {
                FilteredGames.Add(game);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
