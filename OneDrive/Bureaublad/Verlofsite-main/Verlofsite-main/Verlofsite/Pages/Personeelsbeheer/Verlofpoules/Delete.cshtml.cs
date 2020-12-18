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
    public class DeleteModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public DeleteModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Verlofpoule Verlofpoule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Verlofpoule = await _context.Verlofpoules.FirstOrDefaultAsync(m => m.VerlofpouleID == id);

            if (Verlofpoule == null)
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

            Verlofpoule = await _context.Verlofpoules.FindAsync(id);

            if (Verlofpoule != null)
            {
                _context.Verlofpoules.Remove(Verlofpoule);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
