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
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(
            IdentiteitsContext _context,
            IAuthorizationService authorizationService,
            UserManager<VerlofsiteUser> userManager)
            : base(_context, authorizationService, userManager)
        {
            _context = Context;
        }

        
        public string NameSort { get; set; }
        public string SoortSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Aanvraag> Aanvragen { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            CurrentFilter = searchString;

            IQueryable<Aanvraag> aanvragenIQ = from s in Context.Aanvragen
                                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
               aanvragenIQ = aanvragenIQ.Where(s => s.WerknemerNaam.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    aanvragenIQ = aanvragenIQ.OrderByDescending(s => s.WerknemerNaam);
                    break;
            }
            var aanvragen = from c in Context.Aanvragen
                           select c;

            var isAuthorized = User.IsInRole(Constants.AanvraagManagersRole) ||
                               User.IsInRole(Constants.AanvraagAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved aanvragen are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                aanvragen = aanvragen.Where(c => c.Status == STATUS.goedgekeurd
                                            || c.OwnerID == currentUserId);
            }

            Aanvragen = await aanvragenIQ.ToListAsync();
        }
    }
}
