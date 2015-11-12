using System;
using System.Collections.Generic;
using System.Linq;

using CorflowMobile.Models;
using Xamarin.Forms;
using CorflowMobile.Data;
using CorflowMobile.Controllers;

namespace CorflowMobile.Superclasses
{
    class SyncItems : NullObjects
    {
        public List<Adres> GetAdresses()
        {
            return (List<Adres>)DependencyService.Get<IDataService>().LoadAll<Adres>();
        }
        public List<Artikel> GetArticles()
        {
            return (List<Artikel>)DependencyService.Get<IDataService>().LoadAll<Artikel>();
        }
        public List<Bedrijf> GetCompanys()
        {
            return (List<Bedrijf>)DependencyService.Get<IDataService>().LoadAll<Bedrijf>();
        }
        public Bedrijf GetCompanyFromAssessment(Opdracht assesment)
        {
            var companys = GetCompanys().Where(t => assesment.BedrijfID == t.ID).ToList();
            if (companys.Count > 0)
                return companys[0];
            else
                return NullBedrijf();
        }
        public Persoon GetContactFromACompany(int companyID)
        {
            var CompanyContactList = DependencyService.Get<IDataService>().LoadAll<BedrijfsPersoon>()
                .Where(t => t.BedrijfID == companyID)
                .ToList();
            if (CompanyContactList.Count > 0)
                return GetPersons()[CompanyContactList[0].ID - 1];
            else
                return NullPerson();
        }
        public List<Persoon> GetAllContactsFromACompany(int companyID)
        {
            var companyContactList = DependencyService.Get<IDataService>().LoadAll<BedrijfsPersoon>()
                .Where(t => t.BedrijfID == companyID)
                .ToList();
            var persons = new List<Persoon>();
            foreach (BedrijfsPersoon bp in companyContactList)
                persons.Add(GetPersons()[bp.PersoonID - 1]);
            return persons;
        }
        public List<Functie> GetFunctions()
        {
            return DependencyService.Get<IDataService>().LoadAll<Functie>().ToList();
        }
        public List<Gemeente> GetCitys()
        {
            return (List<Gemeente>)DependencyService.Get<IDataService>().LoadAll<Gemeente>();
        }
        public Gemeente GetCityByAdress(int adresID)
        {
            return GetCitys()[GetAdresses()[adresID].Gemeente];
        }
        public List<Ligplaats> GetStockPlaces()
        {
            return (List<Ligplaats>)DependencyService.Get<IDataService>().LoadAll<Ligplaats>();
        }
        public List<Opdracht> GetAssignments()
        {
            return (List<Opdracht>)DependencyService.Get<IDataService>().LoadAll<Opdracht>();
        }
        public List<Opdracht> GetAssignmentsForLogedInPersonByDate(DateTime date)
        {
            var assignmentWorker = DependencyService.Get<IDataService>().LoadAll<OpdrachtWerknemer>()
                .Where(t => t.Datum == date && t.WerknemerID == LoginController.Instance.GetCurrentUser.ID)
                .ToList();
            var assignments = new List<Opdracht>();
            foreach(OpdrachtWerknemer opw in assignmentWorker)
                assignments.Add(DependencyService.Get<IDataService>().LoadAll<Opdracht>().ToList()[opw.OpdrachtID]);
            return assignments;
                
        }
        public List<Persoon> GetPersons()
        {
            return (List<Persoon>)DependencyService.Get<IDataService>().LoadAll<Persoon>();
        }
        public List<Persoon> GetContactPersons()
        {
            return GetPersons().Where(t => t.Functie == 0).ToList();
        }
        public List<Prestatie> GetAchievements()
        {
            return (List<Prestatie>)DependencyService.Get<IDataService>().LoadAll<Prestatie>();
        }
        public List<Prestatie> GetAchievementsFromAssessment(Opdracht assessment)
        {
            return DependencyService.Get<IDataService>().LoadAll<Prestatie>().Where(t => t.OpdrachtID == assessment.ID).ToList();
        }
        public List<Prestatiesoort> GetActivityType()
        {
            return (List<Prestatiesoort>)DependencyService.Get<IDataService>().LoadAll<Prestatiesoort>();
        }
        public List<VerbruikLigplaats> GetUsedArticlesByStockPlace(Ligplaats ligplaats)
        {
            return DependencyService.Get<IDataService>().LoadAll<VerbruikLigplaats>()
                .Where(t => t.LigplaatsID == ligplaats.ID)
                .ToList();
        }
        public List<Verbruiksartikel> GetUsedArticles()
        {
            return (List<Verbruiksartikel>)DependencyService.Get<IDataService>().LoadAll<Verbruiksartikel>();
        }
        public List<Verbruiksartikel> GetUsedArticlesByAssignment(Opdracht opdracht)
        {
            return DependencyService.Get<IDataService>().LoadAll<Verbruiksartikel>()
                .Where(t => t.OpdrachtID == opdracht.ID)
                .ToList();
        }
        public List<VerkochtArtikel> GetSoldArticlesByCompany(Bedrijf bedrijf)
        {
            return DependencyService.Get<IDataService>().LoadAll<VerkochtArtikel>()
                .Where(t => t.Bedrijf == bedrijf.ID)
                .ToList();
        }
        public List<Verslag> GetReportsByAchievement(Prestatie prestatie)
        {
            return DependencyService.Get<IDataService>().LoadAll<Verslag>()
                .Where(t => t.Prestatie == prestatie.ID)
                .ToList();
        }
        public string FormattedAddress(int companyID)
        {
            return GetAdresses()[GetCompanys()[companyID].Adres].Straat + " " + GetAdresses()[GetCompanys()[companyID].Adres].Nummer
                    + ", " + GetCityByAdress(GetCompanys()[companyID].Adres).Postcode + " " + GetCityByAdress(GetCompanys()[companyID].Adres).Plaats;
        }
    }
}
