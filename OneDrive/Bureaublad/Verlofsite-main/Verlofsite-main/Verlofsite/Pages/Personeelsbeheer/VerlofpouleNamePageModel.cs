using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Verlofsite.Data;

namespace Verlofsite.Pages.Personeelsbeheer
{
    public class VerlofsiteUserNamePageModel : PageModel
    {
        public SelectList VerlofpouleNameSL { get; set; }
        public void PopulateVerlofpoulesDropDownList(IdentiteitsContext _context,
                   object selectedVerlofpoule = null)
        {
            var verlofpoulesQuery = from d in _context.Verlofpoules
                                   orderby d.Naam // Sort by name.
                                   select d;

            VerlofpouleNameSL = new SelectList(verlofpoulesQuery,
                        "VerlofpouleID", "Naam", selectedVerlofpoule);
        }
    }
}