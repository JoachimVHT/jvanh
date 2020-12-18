using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.Personeelsbeheer.Inschrijvingen
{
    public class EditModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public EditModel(Verlofsite.Data.IdentiteitsContext context)
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
           ViewData["VerlofpouleID"] = new SelectList(_context.Verlofpoules, "VerlofpouleID", "VerlofpouleID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Inschrijving).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InschrijvingExists(Inschrijving.InschrijvingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InschrijvingExists(int id)
        {
            return _context.Inschrijvingen.Any(e => e.InschrijvingID == id);
        }
    }
}
