using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GroupNineMobileProject
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AppTheme> AppThemes { get; } = 
            new ObservableCollection<AppTheme> { AppTheme.Light, AppTheme.Dark };

        private AppTheme _selectedAppTheme;
        public AppTheme SelectedAppTheme
        {
            get => _selectedAppTheme;
            set
            {
                _selectedAppTheme = value;
                OnPropertyChanged();
                ApplyTheme(value.ToString()); // Convert AppTheme to string  
                Preferences.Set("Theme", value.ToString()); // Save theme preference as string  
            }
        }

        private bool _notificationsEnabled;
        public bool NotificationsEnabled
        {
            get => _notificationsEnabled;
            set { _notificationsEnabled = value; OnPropertyChanged(); }
        }

        private bool _syncEnabled;
        public bool SyncEnabled
        {
            get => _syncEnabled;
            set { _syncEnabled = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand => new Command(SaveSettings);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ApplyTheme(string theme)
        {
            if (theme == "Dark")
                App.Current.Resources["PrimaryColor"] = Colors.Black;
            else if (theme == "Light")
                App.Current.Resources["PrimaryColor"] = Colors.White;
            else
                App.Current.Resources["PrimaryColor"] = Colors.Gray; // Default

            App.Current.MainPage.BackgroundColor = (Color)App.Current.Resources["PrimaryColor"];
        }

        private void SaveSettings()
        {
            App.Current.MainPage.DisplayAlert("Success", "Settings saved!", "OK");
        }

        public SettingsViewModel()
        {
            // Load saved theme  
            var savedTheme = Preferences.Get("Theme", "System Default");
            if (Enum.TryParse(savedTheme, out AppTheme theme))
            {
                SelectedAppTheme = theme;
            }
            else
            {
                SelectedAppTheme = AppTheme.Unspecified;
            }
            ApplyTheme(SelectedAppTheme.ToString());
        }
    }
}
