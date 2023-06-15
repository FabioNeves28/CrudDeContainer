using CrudeDeContainer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudeDeContainer.Controllers
{
    public class ReportsController : Controller
    {
        private readonly BancoDeDados _context;
        public ReportsController(BancoDeDados context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var report = new List<Report>();
            var clientes = _context.Clientes.ToList();
            foreach(var cliente in clientes)
            {
                var clienteReport = new Report();
                clienteReport.Cliente = cliente.Nome;
                var containers = _context.Containers.Include(c => c.Cliente).Where(c => c.ClienteID == cliente.ID).ToList();
                var movimentacoes = _context.Movements.Where(m => containers.Select(c => c.ID).Contains(m.ContainerID)).ToList();
                foreach (var movement in movimentacoes)
                {
                    var tipo = movement.Tipo.Trim().ToLower();

                    if (tipo == "embarque")
                        clienteReport.Embarque++;
                    else if (tipo == "descarga")
                        clienteReport.Descarga++;
                    else if (tipo == "gate in")
                        clienteReport.GateIn++;
                    else if (tipo == "gate out")
                        clienteReport.GateOut++;
                    else if (tipo == "reposicionamento")
                        clienteReport.Reposicionamento++;
                    else if (tipo == "pesagem")
                        clienteReport.Pesagem++;
                    else if (tipo == "scanner")
                        clienteReport.Scanner++;
                }
                clienteReport.Importações = containers.Count(c => c.Categoria.Trim().ToLower() == "importação");
                clienteReport.Exportações = containers.Count(c => c.Categoria.Trim().ToLower() == "exportação");
                report.Add(clienteReport);
            }
            return View(report);
        }
    }
}
