
using CorflowMobile.Data;
using CorflowMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CorflowMobile.Controllers
{
    public class DataController
    {
        private static DataController instance;

        private NullObjects _nullObjects = new NullObjects();

        public static DataController Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataController();

                return instance;
            }
        }

        public void Update(object item)
        {
            DependencyService.Get<IDataService>().Update(item);
        }

        public void Insert(object item)
        {
            DependencyService.Get<IDataService>().Insert(item);
        }

        public List<Artikel> GetArticles()
        {
            return (List<Artikel>)DependencyService.Get<IDataService>().LoadAll<Artikel>();
        }
        public List<Bedrijf> GetCompanys()
        {
            return (List<Bedrijf>)DependencyService.Get<IDataService>().LoadAll<Bedrijf>();
        }
        public List<BedrijfsPersoon> GetCompanyPersons()
        {
            return (List<BedrijfsPersoon>)DependencyService.Get<IDataService>().LoadAll<BedrijfsPersoon>();
        }
        public List<Functie> GetFunctions()
        {
            return (List<Functie>)DependencyService.Get<IDataService>().LoadAll<Functie>();
        }
        public List<Ligplaats> GetStockplaces()
        {
            return (List<Ligplaats>)DependencyService.Get<IDataService>().LoadAll<Ligplaats>();
        }
        public List<VerbruikLigplaats> GetUsedStockplaces()
        {
            return (List<VerbruikLigplaats>)DependencyService.Get<IDataService>().LoadAll<VerbruikLigplaats>();
        }
        public List<Persoon> GetPersons()
        {
            return (List<Persoon>)DependencyService.Get<IDataService>().LoadAll<Persoon>();
        }
        public List<Prestatie> GetAchievements()
        {
            return (List<Prestatie>)DependencyService.Get<IDataService>().LoadAll<Prestatie>();
        }
        public List<Verbruiksartikel> GetUsedArticles()
        {
            return (List<Verbruiksartikel>)DependencyService.Get<IDataService>().LoadAll<Verbruiksartikel>();
        }
        public List<Adres> GetAddresses()
        {
            return (List<Adres>)DependencyService.Get<IDataService>().LoadAll<Adres>();
        }
        public List<Gemeente> GetCitys()
        {
            return (List<Gemeente>)DependencyService.Get<IDataService>().LoadAll<Gemeente>();
        }
        public List<Prestatiesoort> GetAchievementtypes()
        {
            return (List<Prestatiesoort>)DependencyService.Get<IDataService>().LoadAll<Prestatiesoort>();
        }
        public List<OpdrachtWerknemer> GetAssessmentEmployers()
        {
            return (List<OpdrachtWerknemer>)DependencyService.Get<IDataService>().LoadAll<OpdrachtWerknemer>();
        }
        public List<Opdracht> GetAssessments()
        {
            return (List<Opdracht>)DependencyService.Get<IDataService>().LoadAll<Opdracht>();
        }
        public List<VerkochtArtikel> GetSoldArticles()
        {
            return (List<VerkochtArtikel>)DependencyService.Get<IDataService>().LoadAll<VerkochtArtikel>();
        }
        public List<Verslag> GetReports()
        {
            return (List<Verslag>)DependencyService.Get<IDataService>().LoadAll<Verslag>();
        }
        public List<PrestatiesPrestatiesoorten> GetAchievementAchievementtypes()
        {
            return (List<PrestatiesPrestatiesoorten>)DependencyService.Get<IDataService>().LoadAll<PrestatiesPrestatiesoorten>();
        }
        public Bedrijf GetCompanyFromAssessment(Opdracht assesment)
        {
            var companys = GetCompanys().Where(t => assesment.BedrijfID == t.ID).ToList();
            if (companys.Count > 0)
                return companys[0];
            else
                return _nullObjects.Bedrijf();
        }
        public Persoon GetContactFromACompany(int companyID)
        {
            var CompanyContactList = GetCompanyPersons().Where(t => t.BedrijfID == companyID).ToList();
            if (CompanyContactList.Count > 0)
                return GetPersons()[CompanyContactList[0].ID - 1];
            else
                return _nullObjects.Persoon();
        }
        public List<Persoon> GetAllContactsFromACompany(int companyID)
        {
            var companyContactList = GetCompanyPersons().Where(t => t.BedrijfID == companyID).ToList();
            var persons = new List<Persoon>();
            foreach (BedrijfsPersoon bp in companyContactList)
                persons.Add(GetPersons()[bp.PersoonID - 1]);
            return persons;
        }
        public Gemeente GetCityByAdress(int adresID)
        {
            return GetCitys()[GetAddresses()[adresID].Gemeente];
        }
        public List<Opdracht> GetAssignmentsForLogedInPersonByDate(DateTime date)
        {
            var assignmentWorker = GetAssessmentEmployers().Where(t => t.Datum == date && t.WerknemerID == LoginController.Instance.GetCurrentUser.ID).ToList();
            var assignments = new List<Opdracht>();
            foreach (OpdrachtWerknemer opw in assignmentWorker)
                assignments.Add(GetAssessments()[opw.OpdrachtID]);
            return assignments;

        }
        public List<Persoon> GetContactPersons()
        {
            return GetPersons().Where(t => t.Functie == 0).ToList();
        }
        public List<Prestatie> GetAchievementsFromAssessment(Opdracht assessment)
        {
            return GetAchievements().Where(t => t.OpdrachtID == assessment.ID).ToList();
        }
        public List<VerbruikLigplaats> GetUsedArticlesByStockPlace(Ligplaats ligplaats)
        {
            return GetUsedStockplaces().Where(t => t.LigplaatsID == ligplaats.ID).ToList();
        }
        public List<Verbruiksartikel> GetUsedArticlesByAssignment(Opdracht opdracht)
        {
            return GetUsedArticles().Where(t => t.OpdrachtID == opdracht.ID).ToList();
        }
        public List<VerkochtArtikel> GetSoldArticlesByCompany(Bedrijf bedrijf)
        {
            return GetSoldArticles().Where(t => t.Bedrijf == bedrijf.ID).ToList();
        }
        public List<Verslag> GetReportsByAchievement(Prestatie prestatie)
        {
            return GetReports().Where(t => t.Prestatie == prestatie.ID).ToList();
        }
        public string FormattedAddress(int companyID)
        {
            return GetAddresses()[GetCompanys()[companyID].Adres].Straat + " " + GetAddresses()[GetCompanys()[companyID].Adres].Nummer
                    + ", " + GetCityByAdress(GetCompanys()[companyID].Adres).Postcode + " " + GetCityByAdress(GetCompanys()[companyID].Adres).Plaats;
        }
        public List<PrestatiesPrestatiesoorten> GetAchievementsAchievementTypeByIDs(int prestatieID, int prestatiesoortID)
        {
            return GetAchievementAchievementtypes().Where(t => t.PrestatieID == prestatieID && t.PrestatieSoortID == prestatiesoortID).ToList();
        }
        public List<PrestatiesPrestatiesoorten> GetAchievementsAchievementTypeByAchievementID(int prestatieID)
        {
            return GetAchievementAchievementtypes().Where(t => t.PrestatieID == prestatieID).ToList();
        }
        public List<BedrijfsPersoon> GetCompanyPersonByPersonID(int persoonID)
        {
            return GetCompanyPersons().Where(t => persoonID == t.PersoonID).ToList();
        }
        public List<OpdrachtWerknemer> GetAssessmentEmployerByEmployerID(int werknemerID)
        {
            return GetAssessmentEmployers().Where(t => t.WerknemerID == LoginController.Instance.GetCurrentUser.ID).ToList();
        }
        public List<Opdracht> GetAssessmentsByCompanyID(int bedrijfID)
        {
            return GetAssessments().Where(t => t.BedrijfID == bedrijfID).ToList();
        }
        public List<Opdracht> GetAssessmentsByID(int opdrachtID)
        {
            return GetAssessments().Where(t => t.ID == opdrachtID).ToList();
        }
    }
}
