using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Verlofsite.Areas.Identity.Data;
using Verlofsite.Authorization;
using Verlofsite.Data;
using Verlofsite.Models;
namespace Verlofsite.Pages.Aanvragen
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(
            IdentiteitsContext context,
            IAuthorizationService authorizationService,
            UserManager<VerlofsiteUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }
        private readonly Verlofsite.Data.IdentiteitsContext _context;
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Aanvraag Aanvraag { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Aanvraag.AanvraagDatum = DateTime.Now;
                return Page();
            }
            Aanvraag.OwnerID = UserManager.GetUserId(User);
            var gebruiker = await UserManager.GetUserAsync(User);
            Aanvraag.WerknemerNaam = gebruiker.Voornaam + " " + gebruiker.Achternaam;
            // requires using AanvraagManager.Authorization;
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                        User, Aanvraag,
                                                        AanvraagOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            Context.Aanvragen.Add(Aanvraag);
            await Context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}