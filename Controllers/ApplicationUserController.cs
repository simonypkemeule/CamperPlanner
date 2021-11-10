using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CamperPlanner.Data;
using CamperPlanner.Models;
using System.ComponentModel.DataAnnotations;
using CamperPlanner.Models.ViewModels;

namespace CamperPlanner.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: ApplicationsUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUsers.ToListAsync());
        }

        // GET: ApplicationsUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = await _context.ApplicationUsers
                            .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: ApplicationsUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationsUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Voornaam,Achternaam,Email,PhoneNumber,Bankrekening,Geboortedatum,Postcode,Straatnaam")] ApplicationUser users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: ApplicationsUser/Edit/5

        

        public async Task<IActionResult> Edit(string id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.userFullName = (user.Voornaam + " " + user.Achternaam);

            var userId = user.Id;
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var voornaam = user.Voornaam;
            var achternaam = user.Achternaam;
            var geboortedatum = user.Geboortedatum;
            var postcode = user.Postcode;
            var huisnummer = user.Huisnummer;
            var bankrekening = user.Bankrekening;
            var email = user.Email;
            var straatnaam = user.Straatnaam;

            EditUserViewModel editUserViewModel = new EditUserViewModel
            {
                UserId = userId,
                RoleName = roles.First(),
                Voornaam = voornaam,
                Achternaam = achternaam,
                Geboortedatum = geboortedatum,
                Postcode = postcode,
                Huisnummer = huisnummer,
                Bankrekening = bankrekening,
                Email = email,
                Straatnaam = straatnaam,
                PhoneNumber = phoneNumber
            };

            return View(editUserViewModel);
        }

        // POST: ApplicationsUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel editUserViewModel)
        {
           
            var user = await _userManager.FindByIdAsync(editUserViewModel.UserId);

            user.Voornaam = editUserViewModel.Voornaam;
            user.Achternaam = editUserViewModel.Achternaam;
            user.Geboortedatum = editUserViewModel.Geboortedatum;
            user.Postcode = editUserViewModel.Postcode;
            user.Straatnaam = editUserViewModel.Straatnaam;
            user.Huisnummer = editUserViewModel.Huisnummer;
            user.Bankrekening = editUserViewModel.Bankrekening;
            user.PhoneNumber = editUserViewModel.PhoneNumber;
            user.Email = editUserViewModel.Email;

            if (editUserViewModel.RoleName == "Admin")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                await _userManager.RemoveFromRoleAsync(user, "User");
            }
            if (editUserViewModel.RoleName == "User")
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "ApplicationUser");
        }

        // GET: ApplicationsUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: ApplicationsUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);
            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}
