using AlmoxApp.Model;
using AlmoxApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace AlmoxApp.ViewModels
{
    public partial class MovSaidaViewModel : ObservableObject
    {


        [ObservableProperty]
        private List<SourceItem> source =
            [
                new SourceItem("Wesley", "Oliveira Gomes", 40),
                new SourceItem("Vanessa", "Numes Gomes", 42),
                new SourceItem("Amanda", "Numes Gomes", 16),
                new SourceItem("Sofia", "Nunes Gomes", 11),
            ];

        [ObservableProperty]
        private List<LeitorQRCode> leitorQRCode = 
            [
                new LeitorQRCode("FUNCIONÁRIO", false),
                new LeitorQRCode("PRODUTO", false),
            ];

        [RelayCommand]
        async Task OnImage1Tapped(string acao)
        {
            var lFuncunc = LeitorQRCode.FirstOrDefault(w => w.Tipo == "FUNCIONÁRIO");
            if (acao == "PRODUTO" && lFuncunc.Leitura == false)
                return;

            var indexOf = LeitorQRCode.IndexOf(LeitorQRCode.Find(p => p.Tipo == acao));
            await Shell.Current.GoToAsync($"{nameof(LeitorQrCode)}", true,
                new Dictionary<string, object>
                {
                    {"Acao", acao }
                });
            LeitorQRCode[indexOf].Leitura = true;
        }

        [RelayCommand]
        async Task OnSendMovimentacao()
        {

        }
    }
}
