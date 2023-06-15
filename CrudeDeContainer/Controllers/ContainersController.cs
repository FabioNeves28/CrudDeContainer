using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudeDeContainer.Models;

namespace CrudeDeContainer.Controllers
{
    public class ContainersController : Controller
    {
        private readonly BancoDeDados _context;

        public ContainersController(BancoDeDados context)
        {
            _context = context;
        }

        // GET: Containers
        public async Task<IActionResult> Index()
        {
            var bancoDeDados = _context.Containers.Include(c => c.Cliente);
            return View(await bancoDeDados.ToListAsync());
        }

        // GET: Containers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Containers == null)
            {
                return NotFound();
            }

            var container = await _context.Containers
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // GET: Containers/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "Nome");
            return View();
        }

        // POST: Containers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClienteID,Número,Tipo,Status,Categoria")] Container container)
        {
            if (ModelState.IsValid)
            {
                _context.Add(container);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "ID", container.ClienteID);
            return View(container);
        }

        // GET: Containers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Containers == null)
            {
                return NotFound();
            }

            var container = await _context.Containers.FindAsync(id);
            if (container == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "ID", container.ClienteID);
            return View(container);
        }

        // POST: Containers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClienteID,Número,Tipo,Status,Categoria")] Container container)
        {
            if (id != container.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(container);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContainerExists(container.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "Nome", container.ClienteID);
            return View(container);
        }

        // GET: Containers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Containers == null)
            {
                return NotFound();
            }

            var container = await _context.Containers
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        // POST: Containers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Containers == null)
            {
                return Problem("Entity set 'BancoDeDados.Containers'  is null.");
            }
            var container = await _context.Containers.FindAsync(id);
            if (container != null)
            {
                _context.Containers.Remove(container);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContainerExists(int id)
        {
          return _context.Containers.Any(e => e.ID == id);
        }
    }
}
