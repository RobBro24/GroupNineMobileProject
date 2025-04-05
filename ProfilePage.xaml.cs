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


    public async void SignOutButton(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    public async void DeleteButton(object sender, EventArgs e)
    {
        await _dbService.Delete(_loggedInProfile);
        await Navigation.PopAsync();
    }
}