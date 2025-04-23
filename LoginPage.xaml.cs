using SQLitePCL;

namespace GroupNineMobileProject;

public partial class LoginPage : ContentPage
{
    private readonly LocalDbService _dbService;

    public LoginPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        Task.Run(async () => listView.ItemsSource = await _dbService.GetProfiles());
    }

    private async void loginButton_Clicked(object sender, EventArgs e)
    {
        string username = usernameEntryField.Text;
        string password = passwordEntryField.Text;

        //Validation checks
        if (string.IsNullOrWhiteSpace(username))
        {
            await DisplayAlert("Validation Error", "Username cannot be empty.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Validation Error", "Password cannot be empty.", "OK");
            return;
        }

        var existingProfile = await _dbService.GetByCredentials(username, password);
        if (existingProfile == null)
        {
            await DisplayAlert("Validation Error", "Your username or password is wrong.", "OK");
            return;
        }

        //Move to profile page and create database
        else
        {
            SessionService.CurrentProfile = existingProfile;
            SessionService.DbService = _dbService;

            await Navigation.PushAsync(new ProfilePage(existingProfile, _dbService));
        }
    }

    //Navigation to Sign Up page
    private async void signUpButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage(_dbService));
    }

    //Lets you easily view and delete accounts/database data for testing purposes
    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var profile = (Profile)e.Item;
        var action = await DisplayActionSheet("Actions", "Cancel", null, null, "Delete");

        switch (action)
        {
            case "Delete":

                await _dbService.Delete(profile);
                listView.ItemsSource = await _dbService.GetProfiles();

                break;
        }
    }

    //Refreshes the listview whenever the page is navigated to, important for when you delete your profile from the ProfilePage
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await _dbService.GetProfiles();
    }
}
