namespace AlmoxApp
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMxM0AzMjM1MkUzMTJFMzlKK2QyalU0d1EyVUgxN0FFdUVENGdDYmY4UWEyZ2poeEhoSWlUcmFSd2JjPQ==");

            InitializeComponent();

            this.MainPage = new AppShell();
        }
    }
}
