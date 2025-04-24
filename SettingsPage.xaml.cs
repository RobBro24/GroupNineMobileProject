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
        // Update the global resource for background color
        if (theme == "Dark")
            Application.Current.Resources["AppBackgroundColor"] = Colors.Black;
        else if (theme == "Light")
            Application.Current.Resources["AppBackgroundColor"] = Colors.LightSlateGray;
        else
            Application.Current.Resources["AppBackgroundColor"] = Colors.DodgerBlue;

        // Ensure all windows/pages reflect the updated theme
        foreach (var window in Application.Current?.Windows ?? Enumerable.Empty<Window>())
        {
            if (window.Page != null)
            {
                window.Page.BackgroundColor = (Color)Application.Current.Resources["AppBackgroundColor"];
            }
        }
    }
    private void NotifSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        // Save the notification preference
        Preferences.Set("NotificationsEnabled", e.Value);

        // Enable or disable notifications
        if (e.Value)
        {
            EnableNotifications();
        }
        else
        {
            DisableNotifications();
        }
    }

    private void EnableNotifications()
    {
        // Logic to enable notifications
        System.Diagnostics.Debug.WriteLine("Notifications enabled.");
    }

    private void DisableNotifications()
    {
        // Logic to disable notifications
        System.Diagnostics.Debug.WriteLine("Notifications disabled.");
    }

}
