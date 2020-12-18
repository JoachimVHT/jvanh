using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.ZiekteAanvragen
{
    public class CreateModel : PageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public CreateModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ZiekteAanvraag ZiekteAanvraag { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ZiekteAanvraag.AanvraagDatum = DateTime.Now;
                return Page();
            }

            _context.ZiekteAanvragen.Add(ZiekteAanvraag);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
