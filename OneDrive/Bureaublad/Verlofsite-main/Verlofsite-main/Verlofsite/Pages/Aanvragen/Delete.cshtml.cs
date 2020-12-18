using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Verlofsite.Areas.Identity.Data;
using Verlofsite.Authorization;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.Aanvragen
{
   public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(
            IdentiteitsContext context,
            IAuthorizationService authorizationService,
            UserManager<VerlofsiteUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Aanvraag Aanvraag { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Aanvraag = await Context.Aanvragen.FirstOrDefaultAsync(
                                                 m => m.AanvraagID == id);

            if (Aanvraag == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Aanvraag,
                                                     AanvraagOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var aanvraag = await Context
                .Aanvragen.AsNoTracking()
                .FirstOrDefaultAsync(m => m.AanvraagID == id);

            if (Aanvraag == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, aanvraag,
                                                     AanvraagOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Aanvragen.Remove(aanvraag);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
