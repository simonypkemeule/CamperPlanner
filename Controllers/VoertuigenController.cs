using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CamperPlanner.Data;
using CamperPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using CamperPlanner.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CamperPlanner.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VoertuigenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public VoertuigenController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        // GET: Voertuigen
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                return View(await _context.Voertuigen
                    .Include(m => m.ApplicationUser)
                    .Include(m => m.Contract)
                    .ToListAsync());
            }
            else
            {
                ViewBag.activeUserId = id;
                ViewBag.user =  await _userManager.FindByIdAsync(id);
                return View(await _context.Voertuigen
                    .Include(m => m.ApplicationUser)
                    .Include(m => m.Contract)
                    .Where(i => i.ApplicationUser.Id == id)
                    .ToListAsync());
            }
        }

        // GET: Voertuigen/Details/5
        public async Task<IActionResult> Details(int? id, string userId)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.userId = userId;
            var voertuigen = await _context.Voertuigen
                .FirstOrDefaultAsync(m => m.VoertuigID == id);
            if (voertuigen == null)
            {
            }
            return View(voertuigen);
        }

        // GET: Voertuigen/Create
        public IActionResult Create(string userId)
        {
            ViewBag.userID = userId;  
            return View();
        }

        // POST: Voertuigen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoertuigID,Kenteken,Type,Lengte,Merk,Stroomaansluiting,UserId")] Voertuigen voertuigen, string userId)
        {
            if (ModelState.IsValid)
            {

                _context.Add(voertuigen);
                await _context.SaveChangesAsync();

                var contract = new Contracten
                {
                    VoertuigId = voertuigen.VoertuigID,
                    StartDatum = DateTime.Now.Date,
                    EindDatum = DateTime.Now.Date.AddYears(1)
                };
                _context.Add(contract);
                await _context.SaveChangesAsync();

                var user = await _userManager.FindByIdAsync(userId);

                await _emailSender.SendEmailAsync(user.Email, "Voertuig Geregistreerd!", $"Er is een voertuig voor je geregistreerd!"); 

                return RedirectToAction("Index", "Voertuigen", new { id = userId});
            }
            return View(voertuigen);
        }

        // GET: Voertuigen/Edit/5
        public async Task<IActionResult> Edit(int? id, string userId)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.userId = userId;
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
        public async Task<IActionResult> Edit(int id, [Bind("VoertuigID,Kenteken,Type,Lengte,Merk,Stroomaansluiting")] Voertuigen voertuigen, string userId)
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
                if (userId == null)
                {
                    return RedirectToAction(nameof(Index));
                } else
                {
                    return RedirectToAction("Index", "Voertuigen", new { id = userId });
                }
                
            }
            return View(voertuigen);
        }

        // GET: Voertuigen/Delete/5
        public async Task<IActionResult> Delete(int? id, string userId)
        {
            ViewBag.userId = userId;
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
        public async Task<IActionResult> DeleteConfirmed(int id, string userId)
        {
            ViewBag.userId = userId;
            var voertuigen = await _context.Voertuigen.FindAsync(id);
            _context.Voertuigen.Remove(voertuigen);
            await _context.SaveChangesAsync();
            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Voertuigen", new { id = userId });
            }
        }

        private bool VoertuigenExists(int id)
        {
            return _context.Voertuigen.Any(e => e.VoertuigID == id);
        }
    }
}
