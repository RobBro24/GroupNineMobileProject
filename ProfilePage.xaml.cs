using SQLite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GroupNineMobileProject;

public partial class ProfilePage : ContentPage
{
    private readonly LocalDbService _dbService;
    private readonly Profile _loggedInProfile;

    public ProfilePage(Profile profile, LocalDbService dbService)
    {
        InitializeComponent();
        _loggedInProfile = profile;
        _dbService = dbService;

        UsernameLabel.Text = $"Username: {profile.Username}";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        UsernameLabel.Text = $"Username: {_loggedInProfile.Username}";

        var loggedGames = await _dbService.GetLoggedGames(_loggedInProfile.Id);
        LoggedGamesListView.ItemsSource = loggedGames;
    }

    private async void OnRatingChanged(object sender, EventArgs e)
    {
        var ratingControl = sender as AlohaKit.Controls.Rating;

        if (ratingControl?.BindingContext is LoggedGames game)
        {
            game.Rating = ratingControl.Value;

            await _dbService.UpdateGame(game);
        }
    }

    private async void RemoveButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is LoggedGames game)
        {
            await _dbService.DeleteGame(game);
            var loggedGames = await _dbService.GetLoggedGames(_loggedInProfile.Id);
            LoggedGamesListView.ItemsSource = loggedGames;
        }
    }

    public async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    public async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        await _dbService.Delete(_loggedInProfile);
        await Navigation.PopAsync();
    }
}