namespace AlmoxApp.Model
{
    public class SourceItem(string name, string sobreNome, double quantidade)
    {
        public string Name { get; set; } = name;
        public string SobreNome { get; set; } = sobreNome;
        public double Quantidade { get; set; } = quantidade;
    }
}
