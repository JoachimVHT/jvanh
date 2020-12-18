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
    public class DetailsModel : DI_BasePageModel
    {
        public DetailsModel(
            IdentiteitsContext context,
            IAuthorizationService authorizationService,
            UserManager<VerlofsiteUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public Aanvraag Aanvraag { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Aanvraag = await Context.Aanvragen.FirstOrDefaultAsync(m => m.AanvraagID == id);

            if (Aanvraag == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.AanvraagManagersRole) ||
                               User.IsInRole(Constants.AanvraagAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != Aanvraag.OwnerID
                && Aanvraag.Status != STATUS.goedgekeurd)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, STATUS status)
        {
            var aanvraag = await Context.Aanvragen.FirstOrDefaultAsync(
                                                      m => m.AanvraagID == id);

            if (aanvraag == null)
            {
                return NotFound();
            }

            var aanvraagOperation = (status == STATUS.goedgekeurd)
                                                       ? AanvraagOperations.Approve
                                                       : AanvraagOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, aanvraag,
                                        aanvraagOperation);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            aanvraag.Status = status;
            Context.Aanvragen.Update(aanvraag);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
