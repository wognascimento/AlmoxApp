using AlmoxApp.Model;
using AlmoxApp.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
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

        new Action(async () =>
        {
            string apiUrl = "https://api.cipolatti.com.br:44366/api/AtendentesAlmox/SelecionarTodos";
            HttpClientHandler handler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            MovSaidaViewModel vm = (MovSaidaViewModel)BindingContext;

            using HttpClient client = new(handler);
            try
            {
                string urlComParametro = $"{apiUrl}";
                HttpResponseMessage response = await client.GetAsync(urlComParametro);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    vm.Atendentes = JsonConvert.DeserializeObject<ObservableCollection<AtendenteAlmoxModel>>(responseBody);
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
        }).Invoke();

    }

    async Task LoadQrCode(string qrCode)
    {
        string parametro = qrCode;
        HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        MovSaidaViewModel vm = (MovSaidaViewModel)BindingContext;
        using HttpClient client = new(handler);
        try
        {

            if(vm.Acao == "FUNCIONÁRIO")
            {
                string apiUrl = "https://api.cipolatti.com.br:44366/api/Funcionario/SelecionarByQrcode";
                string urlComParametro = $"{apiUrl}?qrcode={parametro}";
                HttpResponseMessage response = await client.GetAsync(urlComParametro);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    vm.Funcionario = JsonConvert.DeserializeObject<FuncionarioModel>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            else if(vm.Acao == "PRODUTO")
            {
                string apiUrl = "https://api.cipolatti.com.br:44366/api/Descricoes/ProdutoAlmox";
                string urlComParametro = $"{apiUrl}?Codcompladicional={parametro}";
                HttpResponseMessage response = await client.GetAsync(urlComParametro);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    vm.Descricao = JsonConvert.DeserializeObject<DescricaoModel>(responseBody);
                    if(vm.Descricao != null)
                    {
                        var found = vm.ProdutosMov.Where(x => x.CodComplAdicional == vm.Descricao.Codcompladicional).FirstOrDefault();
                        if(found != null)
                        {
                            int indice = vm.ProdutosMov.IndexOf(found);
                            vm.ProdutosMov[indice].Quantidade++;
                            //listView.EndRefresh();
                        }
                        else
                            vm.ProdutosMov.Add(new ProdutoMovModel( vm.Descricao.Planilha, vm.Descricao.DescricaoCompleta, vm.Descricao.Codcompladicional, 1));
                    }
                }
                else
                {
                    Console.WriteLine($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }

            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }

    }
}