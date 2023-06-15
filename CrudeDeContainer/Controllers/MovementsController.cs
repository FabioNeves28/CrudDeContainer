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
    public class MovementsController : Controller
    {
        private readonly BancoDeDados _context;

        public MovementsController(BancoDeDados context)
        {
            _context = context;
        }

        // GET: Movements
        public async Task<IActionResult> Index()
        {
            var bancoDeDados = _context.Movements.Include(m => m.Container);
            return View(await bancoDeDados.ToListAsync());
        }

        // GET: Movements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movements == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements
                .Include(m => m.Container)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movement == null)
            {
                return NotFound();
            }

            return View(movement);
        }

        // GET: Movements/Create
        public IActionResult Create()
        {
            ViewData["ContainerID"] = new SelectList(_context.Containers.Include(c => c.Cliente), "ID", "Cliente.Nome");
            return View();
        }

        // POST: Movements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ContainerID,Tipo,Inicio,Fim")] Movement movement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContainerID"] = new SelectList(_context.Containers, "ID", "ID", movement.ContainerID);
            return View(movement);
        }

        // GET: Movements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movements == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements.FindAsync(id);
            if (movement == null)
            {
                return NotFound();
            }
            ViewData["ContainerID"] = new SelectList(_context.Containers, "ID", "ID", movement.ContainerID);
            return View(movement);
        }

        // POST: Movements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ContainerID,Tipo,Inicio,Fim")] Movement movement)
        {
            if (id != movement.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovementExists(movement.ID))
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
            ViewData["ContainerID"] = new SelectList(_context.Containers.Include(c => c.Cliente), "ID", "Cliente.Nome", movement.ContainerID);
            return View(movement);
        }

        // GET: Movements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movements == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements
                .Include(m => m.Container)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movement == null)
            {
                return NotFound();
            }

            return View(movement);
        }

        // POST: Movements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movements == null)
            {
                return Problem("Entity set 'BancoDeDados.Movements'  is null.");
            }
            var movement = await _context.Movements.FindAsync(id);
            if (movement != null)
            {
                _context.Movements.Remove(movement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovementExists(int id)
        {
          return _context.Movements.Any(e => e.ID == id);
        }
    }
}
