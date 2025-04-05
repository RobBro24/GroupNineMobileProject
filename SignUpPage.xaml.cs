using SQLitePCL;

namespace GroupNineMobileProject
{
    public partial class SignUpPage : ContentPage
    {
        private readonly LocalDbService _dbService;

        public SignUpPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
        }

        private async void signUpButton_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntryField.Text;
            string email = emailEntryField.Text;
            string password = passwordEntryField.Text;
            string passwordConfirm = passwordConfirmEntryField.Text;

            //Validation checks
            if (string.IsNullOrWhiteSpace(username))
            {
                await DisplayAlert("Validation Error", "Username cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Validation Error", "Email cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Validation Error", "Password cannot be empty.", "OK");
                return;
            }

            if (password != passwordConfirm)
            {
                await DisplayAlert("Validation Error", "Passwords must match.", "OK");
                return;
            }

            var existingProfile = await _dbService.GetByEmail(email);
            if (existingProfile != null)
            {
                await DisplayAlert("Validation Error", "This email is already attatched to an account.", "OK");
                return;
            }

            //Create profile in database
            else
            {
                await _dbService.Create(new Profile
                {
                    Username = username,
                    Email = email,
                    Password = password
                });

                await DisplayAlert("Account successfully created", "Please navigate to the login page", "OK");
            }

            //Refresh database and clear input fields
            listView.ItemsSource = await _dbService.GetProfiles();

            usernameEntryField.Text = string.Empty;
            emailEntryField.Text = string.Empty;
            passwordEntryField.Text = string.Empty;
            passwordConfirmEntryField.Text = string.Empty;
        }

        //Login page navigation
        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(_dbService));
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
    }

}
