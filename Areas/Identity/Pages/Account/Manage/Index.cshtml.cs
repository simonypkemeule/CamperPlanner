using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CamperPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamperPlanner.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel
        {
            public string userId { get; set; }

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
            var userId = user.Id;
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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
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
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
