using Microsoft.Maui.Controls;
using System.Linq;
using System.Threading.Tasks;

namespace GroupNineMobileProject
{
    public partial class HomePage : ContentPage
    {
        public static readonly BindableProperty RandomGameProperty =
            BindableProperty.Create(
                nameof(RandomGame),
                typeof(Game),
                typeof(HomePage),
                default(Game));

        public Game? RandomGame
        {
            get => (Game?)GetValue(RandomGameProperty);
            set => SetValue(RandomGameProperty, value);
        }

        private readonly LocalDbService _dbService;

        public HomePage()
        {
            InitializeComponent();
            _dbService = SessionService.DbService;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ShowRandomGameAsync();
            await ShowLatestLoggedGameAsync();
        }

        private async Task ShowRandomGameAsync()
        {
            var viewModel = new GamesViewModel();
            var game = await viewModel.GetRandomGameAsync();

            RandomGame = game ?? new Game { Name = "No games found", Summary = "" };
        }

        private async Task ShowLatestLoggedGameAsync()
        {
            var profile = SessionService.CurrentProfile;

            if (profile != null)
            {
                var loggedGames = await _dbService.GetLoggedGames(profile.Id);
                var latestGame = loggedGames.OrderByDescending(g => g.DateLogged).FirstOrDefault();

                if (latestGame != null)
                {
                    LatestGameLabel.Text = "Last Logged Game:";
                    LatestGameFrame.BindingContext = latestGame;
                    LatestGameFrame.IsVisible = true;
                }
                else
                {
                    LatestGameLabel.Text = "No games logged yet.";
                    LatestGameFrame.IsVisible = false;
                }
            }
            else
            {
                LatestGameLabel.Text = "Please log in to see your most recent game.";
                LatestGameFrame.IsVisible = false;
            }
        }
    }
}
