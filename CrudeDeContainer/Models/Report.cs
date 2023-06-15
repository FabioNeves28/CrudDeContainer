namespace CrudeDeContainer.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public int Importações { get; set; }
        public int Exportações { get; set; }
        public int Embarque { get; set; }
        public int Descarga { get; set; }
        public int GateIn { get; set; }
        public int GateOut { get; set;}
        public int Reposicionamento { get; set; }
        public int Pesagem { get; set; }
        public int Scanner { get; set; }
    }
}
