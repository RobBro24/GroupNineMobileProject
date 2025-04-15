using System;
using Microsoft.Maui.Controls;

namespace GroupNineMobileProject
{
    public partial class GamesPage : ContentPage
    {
        private readonly GamesViewModel _viewModel;

        public GamesPage()
        {
            InitializeComponent();
            _viewModel = new GamesViewModel();
            BindingContext = _viewModel;

            // Load all games as soon as the page loads
            LoadAllGames();
        }

        // Method to load all games
        private async void LoadAllGames()
        {
            await _viewModel.LoadGamesAsync("Halo"); // You can use any default search query here.
        }
    }
}
