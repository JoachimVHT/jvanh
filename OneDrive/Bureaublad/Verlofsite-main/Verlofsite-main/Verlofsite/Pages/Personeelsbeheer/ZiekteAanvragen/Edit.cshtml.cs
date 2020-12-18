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

namespace Verlofsite.Pages.ZiekteAanvragen
{
    public class EditModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public EditModel(Verlofsite.Data.IdentiteitsContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ZiekteAanvraag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZiekteAanvraagExists(ZiekteAanvraag.ZiekteAanvraagID))
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

        private bool ZiekteAanvraagExists(int id)
        {
            return _context.ZiekteAanvragen.Any(e => e.ZiekteAanvraagID == id);
        }
    }
}
