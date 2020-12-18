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
    public class Index2Model : DI_BasePageModel
    {
        public Index2Model(
            IdentiteitsContext context,
            IAuthorizationService authorizationService,
            UserManager<VerlofsiteUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IList<Aanvraag> Aanvraag { get; set; }

        public async Task OnGetAsync()
        {
            var aanvragen = from c in Context.Aanvragen
                           select c;


            var currentUserId = UserManager.GetUserId(User);

           
                aanvragen = aanvragen.Where(c =>  c.OwnerID == currentUserId);
            

            Aanvraag = await aanvragen.ToListAsync();
        }
    }
}
