using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.Personeelsbeheer.Verlofpoules
{
    public class IndexModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public IndexModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

        public IList<Verlofpoule> Verlofpoule { get;set; }

        public async Task OnGetAsync()
        {
            Verlofpoule = await _context.Verlofpoules.ToListAsync();
        }
    }
}
