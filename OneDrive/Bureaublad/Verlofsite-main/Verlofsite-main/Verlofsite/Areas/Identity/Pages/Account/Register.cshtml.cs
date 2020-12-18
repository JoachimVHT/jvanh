using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Verlofsite.Areas.Identity.Data;

namespace Verlofsite.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "AanvraagAdministrators")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<VerlofsiteUser> _signInManager;
        private readonly UserManager<VerlofsiteUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        

        public RegisterModel(
            UserManager<VerlofsiteUser> userManager,
            SignInManager<VerlofsiteUser> signInManager,
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
            [Display(Name = "Voornaam")]
            public string Voornaam { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Achternaam")]
            public string Achternaam { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Functie")]
            public string Functie { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Rol")]
            public ROL Rol { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new VerlofsiteUser {
                    Voornaam = Input.Voornaam,
                    Achternaam = Input.Achternaam,
                    Functie = Input.Functie,
                    Rol = Input.Rol,
                    UserName = Input.Email,
                    Email = Input.Email,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if(user.Rol == ROL.ploegchef)
                {
                    await _userManager.AddToRoleAsync(user, "AanvraagManagers");
                }
                if (user.Rol == ROL.productieverantwoordelijke)
                {
                    await _userManager.AddToRoleAsync(user, "AanvraagAdministrators");

                }
                if (result.Succeeded)
                {
                    
                    _logger.LogInformation("nieuw account met wachtwoord gemaakt");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

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
