using AlmoxApp.ViewModels;
namespace AlmoxApp.Views;

[QueryProperty(nameof(QrCode), "qrCode")]
public partial class MovSaida : ContentPage
{

    public string QrCode
    {
        set { LoadQrCode(value); }
    }

    public MovSaida(MovSaidaViewModel movSaidaViewModel)
	{
        InitializeComponent();

        BindingContext = movSaidaViewModel;

    }

    void LoadQrCode(string qrCode)
    {
        System.Diagnostics.Debug.WriteLine("***********2 pass data  ******");

        try
        {
            
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to load animal.");
        }
    }
}