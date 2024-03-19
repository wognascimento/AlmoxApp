using AlmoxApp.Views;

namespace AlmoxApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MovSaida), typeof(MovSaida));
            Routing.RegisterRoute(nameof(LeitorQrCode), typeof(LeitorQrCode));
        }
    }
}