using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Verlofsite.Areas.Identity.Data;

namespace Verlofsite.Models
{
    public class Verlofpoule
    {
        public int VerlofpouleID { get; set; }

        public String Naam { get; set; }

     

        public ICollection<Inschrijving> Inschrijvingen { get; set; }

    }
}
