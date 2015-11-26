using CorflowMobile.Controllers;
using CorflowMobile.Data;
using CorflowMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
    class ContactData
    {
        public static List<Contact> GetData(List<Persoon> personen)
        {
            List<Contact> contacts = new List<Contact>();
            string company = "";
            if (personen.Count > 0)
            {
                foreach (Persoon p in personen)
                {
                    List<BedrijfsPersoon> bedrijfsPersonen = DataController.Instance.GetCompanyPersonByPersonID(p.ID);
                    List<Bedrijf> bedrijven = DataController.Instance.GetCompanys();
                    if (bedrijfsPersonen.Count > 0)
                        company = bedrijven[bedrijfsPersonen[0].BedrijfID].Naam;
                    contacts.Add(new Contact
                    {
                        ImageSource = "face.png",
                        Name = p.Voornaam + " " + p.Naam,
                        Company = company,
                        Email = p.Email,
                        Phone = p.TelefoonWerk,
						Functie = p.Functie,
						PersoonID = p.ID
                    });
					company = "";
                }
            } else
            {
                contacts.Add(new Contact
                {
                    ImageSource = "face.png",
                    Name = "geen Contacten gevonden",
                    Company = "",
                    Email = "",
                    Phone = "",
					Functie = 0,
					PersoonID = 0
							
                });
            }
            return contacts;
        }
    }
}
