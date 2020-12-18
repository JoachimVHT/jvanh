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
    public class DeleteModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public DeleteModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ZiekteAanvraag = await _context.ZiekteAanvragen.FindAsync(id);

            if (ZiekteAanvraag != null)
            {
                _context.ZiekteAanvragen.Remove(ZiekteAanvraag);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
