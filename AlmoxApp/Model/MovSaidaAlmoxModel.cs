namespace AlmoxApp.Model
{
    public class MovSaidaAlmoxModel
    {
        public int? CodMovimentacao { get; set; }
        public int? CodSaidaAlmox { get; set; }
        public string Barcode { get; set; }
        public double Qtde { get; set; }
        public DateTime Data { get; set; }
        /*public DateTime Hora { get; set; }*/
        public string Resp { get; set; }
        public int? Requisicao { get; set; } = 0;
        public string Destino { get; set; }
        /*public string IncluidoPor { get; set; }*/
        /*public DateTime DataInclusao { get; set; }*/
        public string Funcionario { get; set; }
        public string Setor { get; set; }
        public string Unidade { get; set; }
        public string Expurgo { get; set; } = "0";
        public string Obs { get; set; }
        public int? NumOs { get; set; }
        public string Endereco { get; set; } = "ALM";
        public int CodComplAdicional { get; set; }
        public int CodFun { get; set; }
    }
}
