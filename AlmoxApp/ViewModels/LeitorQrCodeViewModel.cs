using CommunityToolkit.Mvvm.ComponentModel;

namespace AlmoxApp.ViewModels
{
    [QueryProperty("Acao", "Acao")]
    public partial class LeitorQrCodeViewModel : ObservableObject
    {
        public LeitorQrCodeViewModel() { }

        [ObservableProperty]
        string acao;
    }
}
