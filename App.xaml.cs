namespace GroupNineMobileProject
{
    public partial class App : Application
    {
        private static LocalDbService _dbService = new LocalDbService();

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static LocalDbService DbService => _dbService;
    }
}