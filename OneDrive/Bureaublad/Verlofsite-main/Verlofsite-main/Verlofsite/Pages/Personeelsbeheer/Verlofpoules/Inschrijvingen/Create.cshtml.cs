using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Verlofsite.Data;
using Verlofsite.Models;

namespace Verlofsite.Pages.Personeelsbeheer.Inschrijvingen
{
    public class CreateModel : VerlofsiteUserNamePageModel
    {
        private readonly Verlofsite.Data.IdentiteitsContext _context;

        public CreateModel(Verlofsite.Data.IdentiteitsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateVerlofpoulesDropDownList(_context);
            ViewData["VerlofpouleID"] = new SelectList(_context.Verlofpoules, "VerlofpouleID", "VerlofpouleID");
            return Page();
        }

        [BindProperty]
        public Inschrijving Inschrijving { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyInschrijving = new Inschrijving();

            if (await TryUpdateModelAsync<Inschrijving>(
                 emptyInschrijving,
                 "inschrijving",   // Prefix for form value.
                 s => s.InschrijvingID, s => s.VerlofpouleID))
            {
                _context.Inschrijvingen.Add(emptyInschrijving);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateVerlofpoulesDropDownList(_context, emptyInschrijving.VerlofpouleID);
            return Page();
        }
    }
}
