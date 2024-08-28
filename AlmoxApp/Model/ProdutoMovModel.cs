namespace AlmoxApp.Model
{
    public class ProdutoMovModel(string planilha, string descricao, int codcompladicional, double quantidade)
    {
        public string Planilha { get; set; } = planilha;
        public string Descricao { get; set; } = descricao;
        public int CodComplAdicional { get; set; } = codcompladicional;
        public double Quantidade { get; set; } = quantidade;
    }
}
