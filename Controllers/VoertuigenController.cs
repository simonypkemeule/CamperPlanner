using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CamperPlanner.Data;
using CamperPlanner.Models;

namespace CamperPlanner.Controllers
{
    public class VoertuigenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoertuigenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Voertuigen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Voertuigen.ToListAsync());
        }

        // GET: Voertuigen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voertuigen = await _context.Voertuigen
                .FirstOrDefaultAsync(m => m.VoertuigID == id);
            if (voertuigen == null)
            {
                return NotFound();
            }

            return View(voertuigen);
        }

        // GET: Voertuigen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Voertuigen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoertuigID,Kenteken,Type,Lengte,Merk")] Voertuigen voertuigen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voertuigen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voertuigen);
        }

        // GET: Voertuigen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voertuigen = await _context.Voertuigen.FindAsync(id);
            if (voertuigen == null)
            {
                return NotFound();
            }
            return View(voertuigen);
        }

        // POST: Voertuigen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoertuigID,Kenteken,Type,Lengte,Merk")] Voertuigen voertuigen)
        {
            if (id != voertuigen.VoertuigID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voertuigen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoertuigenExists(voertuigen.VoertuigID))
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
            return View(voertuigen);
        }

        // GET: Voertuigen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voertuigen = await _context.Voertuigen
                .FirstOrDefaultAsync(m => m.VoertuigID == id);
            if (voertuigen == null)
            {
                return NotFound();
            }

            return View(voertuigen);
        }

        // POST: Voertuigen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voertuigen = await _context.Voertuigen.FindAsync(id);
            _context.Voertuigen.Remove(voertuigen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoertuigenExists(int id)
        {
            return _context.Voertuigen.Any(e => e.VoertuigID == id);
        }
    }
}
