using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Verlofsite.Areas.Identity.Data;
using Verlofsite.Data;
using Verlofsite.Pages.Aanvragen;

namespace Verlofsite.Pages
{
    [Authorize(Roles = "AanvraagAdministrators")]
    public class PersoneelsbeheerModel : DI_BasePageModel

    {
        private readonly ILogger<PersoneelsbeheerModel> _logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<VerlofsiteUser> userManager;

        public PersoneelsbeheerModel(
            IdentiteitsContext context,
            IAuthorizationService authorizationService,
            ILogger<PersoneelsbeheerModel> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<VerlofsiteUser> userManager)
            : base(context, authorizationService, userManager)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IList<VerlofsiteUser> Gebruiker { get; set; }

        public async Task OnGetAsync()
        {
            var gebruikers = from g in Context.Gebruiker
                             select g;



            Gebruiker = await gebruikers.ToListAsync();
        }
    }
}

