using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Verlofsite.Models;

namespace Verlofsite.Areas.Identity.Data
{
    
        public enum ROL
        {
            arbeider,
            ploegchef,
            productieverantwoordelijke
        }
    // Add profile data for application users by adding properties to the VerlofsiteUser class
    public class VerlofsiteUser : IdentityUser
    {

        [PersonalData]
            public string Voornaam { get; set; }

            [PersonalData]
            public string Achternaam { get; set; }

            [PersonalData]
            public string VolledigeNaam
            {
                get
                {
                    return Voornaam + " " + Achternaam;
                }

            }

            [PersonalData]
            public string Functie { get; set; }

            [PersonalData]
            public ROL Rol { get; set; }

            [PersonalData]
            public ICollection<Aanvraag> Aanvragen { get; set; }

            [PersonalData]

            public ICollection<VerlofsiteUser> Inschrijving { get; set; }

     

    }
    }

