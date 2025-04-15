namespace GroupNineMobileProject;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
       
        ThemePicker.SelectedIndex = Preferences.Get("Theme", 1);
        NotifSwitch.IsToggled = Preferences.Get("Notifications", true);
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        
        // Save preferences
        Preferences.Set("ThemeIndex", ThemePicker.SelectedIndex);
        Preferences.Set("NotificationsEnabled", NotifSwitch.IsToggled);
        

        // Apply theme dynamically
        ApplyTheme(ThemePicker.SelectedItem.ToString());

        DisplayAlert("Success", "Settings saved!", "OK");
    }

    private void ApplyTheme(string theme)
    {
        if (theme == "Dark")
            App.Current.Resources["PrimaryColor"] = Colors.Black;
        else if (theme == "Light")
            App.Current.Resources["PrimaryColor"] = Colors.AntiqueWhite;
        else
            App.Current.Resources["PrimaryColor"] = Colors.Orange;

        BackgroundColor = (Color)App.Current.Resources["PrimaryColor"];
    }
}
