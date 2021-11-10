using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CamperPlanner.Models;
using CamperPlanner.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace CamperPlanner.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name ="Voornaam")]
            public string Voornaam { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Achternaam")]
            public string Achternaam { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Postcode")]
            public string Postcode { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Straatnaam")]
            public string Straatnaam { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Huisnummer")]
            public int Huisnummer { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Bankrekening")]
            public string Bankrekening { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Geboortedatum")]
            public DateTime Geboortsedatum { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Telefoonnummer")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string RoleName { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    Voornaam = Input.Voornaam,
                    Achternaam = Input.Achternaam,
                    Geboortedatum = Input.Geboortsedatum,
                    Postcode = Input.Postcode,
                    Straatnaam = Input.Straatnaam,
                    Huisnummer = Input.Huisnummer,
                    Bankrekening = Input.Bankrekening,
                    PhoneNumber = Input.PhoneNumber,
                    UserName = Input.Email, 
                    Email = Input.Email,
                };
                var result = await _userManager.CreateAsync((ApplicationUser)user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //Add user to role
                    var role = await _userManager.AddToRoleAsync(user, Input.RoleName);

                    _userManager.Options.SignIn.RequireConfirmedAccount = false;
                    
                    await _emailSender.SendEmailAsync(Input.Email, "Registratie bij CamperPlanner", $"Welkom bij CamperPlanner");
                    
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
