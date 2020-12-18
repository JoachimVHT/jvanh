using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.ZiekteAanvragen
{
    [Authorize(Roles = "AanvraagAdministrators")]
    public class IndexModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public IndexModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

        public IList<ZiekteAanvraag> ZiekteAanvraag { get;set; }

        public async Task OnGetAsync()
        {
            ZiekteAanvraag = await _context.ZiekteAanvragen.ToListAsync();
        }
    }
}
