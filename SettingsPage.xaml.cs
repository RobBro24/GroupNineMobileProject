namespace GroupNineMobileProject;

public partial class SettingsPage : ContentPage
{
        public SettingsPage()
        {
            InitializeComponent();

            // Load saved settings
            ThemePicker.SelectedIndex = Preferences.Get("ThemeIndex", 1); // Default to Light theme
            NotifSwitch.IsToggled = Preferences.Get("NotificationsEnabled", true); // Default to notifications enabled
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            // Save preferences
            Preferences.Set("ThemeIndex", ThemePicker.SelectedIndex);
            Preferences.Set("NotificationsEnabled", NotifSwitch.IsToggled);

            // Apply theme dynamically across the app
            var selectedTheme = ThemePicker.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedTheme))
            {
                ApplyThemeGlobally(selectedTheme);
            }

            // Display confirmation
            DisplayAlert("Success", "Settings have been saved!", "OK");
        }

        private void ApplyThemeGlobally(string theme)
        {
            // Change the global background color based on the selected theme
            if (theme == "Dark")
                Application.Current.Resources["AppBackgroundColor"] = Colors.Black;
            else if (theme == "Light")
                Application.Current.Resources["AppBackgroundColor"] = Colors.AntiqueWhite;
            else
                Application.Current.Resources["AppBackgroundColor"] = Colors.Orange;

            // Apply the new background color to all pages dynamically
            App.Current.MainPage.BackgroundColor = (Color)Application.Current.Resources["AppBackgroundColor"];
        }
    }
