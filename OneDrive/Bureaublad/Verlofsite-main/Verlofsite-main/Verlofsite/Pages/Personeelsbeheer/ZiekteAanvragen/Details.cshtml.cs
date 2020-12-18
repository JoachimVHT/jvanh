using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.ZiekteAanvragen
{
    public class DetailsModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public DetailsModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

        public ZiekteAanvraag ZiekteAanvraag { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ZiekteAanvraag = await _context.ZiekteAanvragen.FirstOrDefaultAsync(m => m.ZiekteAanvraagID == id);

            if (ZiekteAanvraag == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
