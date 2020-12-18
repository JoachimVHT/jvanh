using Verlofsite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Verlofsite.Areas.Identity.Data;

namespace Verlofsite.Pages.Aanvragen
{
    public class DI_BasePageModel : PageModel
    {
        protected IdentiteitsContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<VerlofsiteUser> UserManager { get; }

        public DI_BasePageModel(
            IdentiteitsContext context,
            IAuthorizationService authorizationService,
            UserManager<VerlofsiteUser> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
    }
}