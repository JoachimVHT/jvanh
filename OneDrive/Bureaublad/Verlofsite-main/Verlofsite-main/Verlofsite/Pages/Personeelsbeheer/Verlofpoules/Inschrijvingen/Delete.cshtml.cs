using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.Personeelsbeheer.Inschrijvingen
{
    public class DeleteModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public DeleteModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Inschrijving Inschrijving { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Inschrijving = await _context.Inschrijvingen
                .Include(i => i.Verlofpoule).FirstOrDefaultAsync(m => m.InschrijvingID == id);

            if (Inschrijving == null)
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

            Inschrijving = await _context.Inschrijvingen.FindAsync(id);

            if (Inschrijving != null)
            {
                _context.Inschrijvingen.Remove(Inschrijving);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
