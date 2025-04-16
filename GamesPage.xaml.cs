using Microsoft.Maui.Controls;

namespace GroupNineMobileProject
{
    public partial class GamesPage : ContentPage
    {
        public GamesPage()
        {
            System.Diagnostics.Debug.WriteLine("Working GamesPage}"); // Log API error

            InitializeComponent();

            // Set the BindingContext to an instance of the ViewModel
            BindingContext = new GamesViewModel();
        }

        protected override async void OnAppearing()
        {
            System.Diagnostics.Debug.WriteLine("OnAppearing");
            System.Diagnostics.Debug.WriteLine("OnAppearing");
            base.OnAppearing();

            // Load games if no games are loaded already
            var viewModel = (GamesViewModel)BindingContext;

            if (viewModel != null && viewModel.Games.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Trigger API");
                await viewModel.LoadAllGamesAsync(); // Trigger the API call
                System.Diagnostics.Debug.WriteLine("Done!");
            }
        }
    }
}


