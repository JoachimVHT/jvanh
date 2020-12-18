using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Verlofsite.Models
{
    public class Aanvraag
    {
        public int AanvraagID { get; set; }

        // user ID from AspNetUser table.
        public string OwnerID { get; set; }

        public String WerknemerNaam { get; set; }

        public DateTime AanvraagDatum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "startdatum")]
        public DateTime StartDatum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "einddatum")]
        public DateTime EindDatum { get; set; }


        public SOORT Soort { get; set; }
        public STATUS Status { get; set; }

        public string UitlegBeslissing { get; set; }

    }
    public enum SOORT
    {
        verlof,
        overuren,
        ADV,
        kleinVerlet
    }

    public enum STATUS
    {
        onbeslist,
        goedgekeurd,
        geweigerd
    }
}
