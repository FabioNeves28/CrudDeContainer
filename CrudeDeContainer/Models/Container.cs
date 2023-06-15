namespace CrudeDeContainer.Models
{
    public class Container
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public string Número { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set;}
        public string Categoria { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
