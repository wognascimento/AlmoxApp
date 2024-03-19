namespace AlmoxApp.Model
{
    public class LeitorQRCode(string tipo, bool leitura)
    {
        public string Tipo { get; set; } = tipo;
        public bool Leitura { get; set; } = leitura;
    }
}
