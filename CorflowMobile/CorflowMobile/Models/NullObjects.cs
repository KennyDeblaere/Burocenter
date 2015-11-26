namespace CorflowMobile.Models
{
    class NullObjects
    {
        public Persoon Persoon()
        {
            return new Persoon() { Naam = "", Voornaam = ""};
        }

        public Bedrijf Bedrijf()
        {
            return new Bedrijf() { Naam = "Geen bedrijf gevonden" };
        }
    }
}
