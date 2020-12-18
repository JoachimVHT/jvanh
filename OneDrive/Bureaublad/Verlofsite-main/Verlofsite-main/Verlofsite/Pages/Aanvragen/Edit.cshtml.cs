using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Verlofsite.Areas.Identity.Data;
using Verlofsite.Authorization;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.Aanvragen
{
    public class EditModel : DI_BasePageModel
    {
        public EditModel(
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
                                                      AanvraagOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Aanvraag from DB to get OwnerID.
            var aanvraag = await Context
                .Aanvragen.AsNoTracking()
                .FirstOrDefaultAsync(m => m.AanvraagID == id);

            if (aanvraag == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, aanvraag,
                                                     AanvraagOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Aanvraag.OwnerID = aanvraag.OwnerID;

            Context.Attach(Aanvraag).State = EntityState.Modified;

            if (Aanvraag.Status == STATUS.goedgekeurd)
            {
                // If the aanvraag is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
                var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                        Aanvraag,
                                        AanvraagOperations.Approve);

                if (!canApprove.Succeeded)
                {
                    Aanvraag.Status = STATUS.onbeslist;
                }
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
