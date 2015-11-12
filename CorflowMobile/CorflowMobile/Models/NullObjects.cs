using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorflowMobile.Models
{
    class NullObjects
    {
        public Persoon NullPerson()
        {
            return new Persoon() { Naam = "", Voornaam = ""};
        }

        public Bedrijf NullBedrijf()
        {
            return new Bedrijf() { Naam = "Geen bedrijf gevonden" };
        }
    }
}
