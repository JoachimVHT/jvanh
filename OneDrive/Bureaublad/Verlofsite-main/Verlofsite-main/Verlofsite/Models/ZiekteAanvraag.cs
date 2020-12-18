using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Verlofsite.Models
{
    public class ZiekteAanvraag
    {
        public int ZiekteAanvraagID { get; set; }

        public String WerknemerNaam { get; set; }

        public DateTime AanvraagDatum  { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "startdatum")]
        public DateTime StartDatum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "einddatum")]
        public DateTime EindDatum { get; set; }


    }
}
