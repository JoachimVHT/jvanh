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
    public class DetailsModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public DetailsModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

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
    }
}
