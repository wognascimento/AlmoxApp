using AlmoxApp.Model;
using AlmoxApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace AlmoxApp.ViewModels
{
    public partial class MovSaidaViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<ProdutoMovModel> produtosMov = [];
        
        [ObservableProperty]
        private ObservableCollection<MovSaidaAlmoxModel> movSaidas = [];

        [ObservableProperty]
        private List<LeitorQRCode> leitorQRCode = 
            [
                new LeitorQRCode("FUNCIONÁRIO", false),
                new LeitorQRCode("PRODUTO", false),
            ];

        [ObservableProperty]
        private FuncionarioModel funcionario;
        
        [ObservableProperty]
        private DescricaoModel descricao;
        

        [ObservableProperty]
        ObservableCollection<AtendenteAlmoxModel> atendentes;

        [ObservableProperty]
        AtendenteAlmoxModel atendente;
        
        [ObservableProperty]
        int? nOrdemServico;

        [ObservableProperty]
        string acao;

        [RelayCommand]
        async Task OnImage1Tapped(string acao)
        {
            Acao = acao;
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

            if (Funcionario == null)
            {
                await App.Current.MainPage.DisplayAlert("Atenção", "Funcionário não foi informado", "OK");
                return; 
            }
            else if (Atendente == null)
            {
                await App.Current.MainPage.DisplayAlert("Atenção", "Atendente não foi informado", "OK");
                return; 
            }
            else if (NOrdemServico == null || NOrdemServico == 0)
            {
                await App.Current.MainPage.DisplayAlert("Atenção", "Número da O.S não foi informado", "OK");
                return; 
            }
            else if(ProdutosMov.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("Atenção", "Não existe produtos para efetuar a saída.", "OK");
                return;
            }


            foreach (ProdutoMovModel item in ProdutosMov)
            {
                MovSaidas.Add(
                    new MovSaidaAlmoxModel
                    {
                        CodFun = Funcionario.CodFun,
                        CodComplAdicional = item.CodComplAdicional,
                        Qtde = item.Quantidade,
                        Data = DateTime.Now.Date,
                        Funcionario = Funcionario.NomeApelido,
                        Destino = Funcionario.Setor,
                        Setor = Funcionario.Setor,
                        Resp = Atendente.NomeFuncionario,
                        NumOs = NOrdemServico,
                    });
            }
            //api/SaidaAlmox/almoxMovSaida
            string apiUrl = "https://api.cipolatti.com.br:44366/api/SaidaAlmox/almoxMovSaida";
            JsonSerializerOptions options = new()
            {
                WriteIndented = true 
            };
            string jsonParametro = System.Text.Json.JsonSerializer.Serialize(MovSaidas, options);
            HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            var parametro = new
            {
                saidas = MovSaidas.ToArray()
            };

            var content = new StringContent(jsonParametro, Encoding.UTF8, "application/json");
            //var jsonContent = JsonConvert.SerializeObject(parametro);
            //var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using HttpClient client = new(handler);
            try
            {
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    await App.Current.MainPage.DisplayAlert("Sucesso", responseBody, "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    Console.WriteLine($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }

        }
    }
}
