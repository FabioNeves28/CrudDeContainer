using System.Data;

namespace CrudeDeContainer.Models
{
    public class Movement
    {
        public int ID { get; set; }
        public int ContainerID { get; set; }
        public string Tipo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }

        public virtual Container Container { get; set; }
    }
}
