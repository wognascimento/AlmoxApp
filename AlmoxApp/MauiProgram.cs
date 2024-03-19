using AlmoxApp.ViewModels;
using AlmoxApp.Views;
using BarcodeScanner.Mobile;
using Telerik.Maui.Controls.Compatibility;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace AlmoxApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseTelerik()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddBarcodeScannerHandler();
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<MovSaida>();
            builder.Services.AddTransient<MovSaidaViewModel>();

            builder.Services.AddTransient<LeitorQrCode>();
            builder.Services.AddTransient<LeitorQrCodeViewModel>();


            return builder.Build();
        }
    }
}
