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

namespace Verlofsite.Pages.Personeelsbeheer.Verlofpoules
{
    public class EditModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public EditModel(Verlofsite.Data.IdentiteitsContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Verlofpoule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerlofpouleExists(Verlofpoule.VerlofpouleID))
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

        private bool VerlofpouleExists(int id)
        {
            return _context.Verlofpoules.Any(e => e.VerlofpouleID == id);
        }
    }
}
