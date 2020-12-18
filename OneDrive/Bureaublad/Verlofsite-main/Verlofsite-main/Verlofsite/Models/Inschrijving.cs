using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Verlofsite.Areas.Identity.Data;

namespace Verlofsite.Models
{
    public class Inschrijving
    {
        public int InschrijvingID { get; set; }
        public int VerlofpouleID { get; set; }
        public int UserName { get; set; }

        public Verlofpoule Verlofpoule { get; set; }
        public VerlofsiteUser VerlofsiteUser { get; set; }

    }
}
