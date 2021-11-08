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

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }

            [Display(Name = "Achternaam")]
            public string Achternaam { get; set; }

            [Display(Name = "Email")]
            public string Email { get; set; }
            [Display(Name = "Straatnaam")]
            public string Straatnaam { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var voornaam = user.Voornaam;
            var achternaam = user.Achternaam;
            var email = user.Email;
            var straatnaam = user.Straatnaam;

            Username = userName;

            Input = new InputModel
            {
                Voornaam = voornaam,
                Achternaam = achternaam,
                Email = email,
                Straatnaam = straatnaam,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.ApplicationUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: ApplicationsUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Voornaam,Achternaam,Email,PhoneNumber,Bankrekening,Geboortedatum,Postcode,Straatnaam")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return View();
            }

            var voornaam = user.Voornaam;
            var achternaam = user.Achternaam;
            var email = user.Email;
            var straatnaam = user.Straatnaam;
            if (Input.Voornaam != voornaam)
            {
                user.Voornaam = Input.Voornaam;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Achternaam != achternaam)
            {
                user.Achternaam = Input.Achternaam;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Email != email)
            {
                user.Email = Input.Email;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Straatnaam != straatnaam)
            {
                user.Straatnaam = Input.Straatnaam;
                await _userManager.UpdateAsync(user);
            }
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToAction("Index", "Voertuigen", new { id = id });
                }
            }
            return View(user);
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
