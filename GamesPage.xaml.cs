
using Microsoft.Maui.Controls;
using AlohaKit;

namespace GroupNineMobileProject
{
    public partial class GamesPage : ContentPage
    {
        private Profile _currentProfile;
        private readonly LocalDbService _dbService;

        public GamesPage()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("GamesPage constructor running.");

            _dbService = SessionService.DbService;

            BindingContext = new GamesViewModel();
        }

        private async void logButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var game = button?.BindingContext as Game;

            if (_currentProfile == null)
            {
                await DisplayAlert("Not Logged In", "Please log in to log a game.", "OK");
                return;
            }

            if (game != null)
            {
                var loggedGame = new LoggedGames
                {
                    ProfileId = _currentProfile.Id,
                    Name = game.Name,
                    Summary = game.Summary,
                    Rating = game.Rating,
                    Publisher = game.InvolvedCompanies?.FirstOrDefault()?.Company?.Name ?? "Unknown"
                };

                await _dbService.LogGame(loggedGame);
                await DisplayAlert("Success", "Game logged!", "OK");
            }
        }


        protected override async void OnAppearing()
        {
            System.Diagnostics.Debug.WriteLine("OnAppearing");
            base.OnAppearing();

            _currentProfile = SessionService.CurrentProfile;

            var viewModel = (GamesViewModel)BindingContext;

            if (viewModel != null && viewModel.Games.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Trigger API");
                await viewModel.LoadAllGamesAsync(); 
                System.Diagnostics.Debug.WriteLine("Done!");
            }
        }

    }
}


