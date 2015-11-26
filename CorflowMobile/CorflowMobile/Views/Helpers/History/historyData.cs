using CorflowMobile.Data;
using CorflowMobile.Models;
using CorflowMobile.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CorflowMobile
{
	public class historyData
	{
		public static List<history> GetData(List<Opdracht> lijstOpdrachten)
		{
			List<history> ListHistory = new List<history> ();
			List<OpdrachtWerknemer> ListOpdrachtWerknemer = DataController.Instance.GetAssessmentEmployerByEmployerID(LoginController.Instance.GetCurrentUser.ID);
            List<Bedrijf> ListBedrijven = DataController.Instance.GetCompanys();

			if (lijstOpdrachten.Count > 0){
			foreach (Opdracht opdracht in lijstOpdrachten) {
				foreach (OpdrachtWerknemer opdrachtwerknemer in ListOpdrachtWerknemer) {
					if (opdracht.ID == opdrachtwerknemer.OpdrachtID) {
						foreach (Bedrijf bedrijf in ListBedrijven) {
							if (opdracht.BedrijfID == bedrijf.ID) {
								ListHistory.Add (new history{ 
									Name = bedrijf.Naam,
									omschrijving = opdracht.Omschrijving,
									opdrachtID = opdracht.ID
								});
							}
						}
					}
				}
				}
			}else{
				ListHistory.Add (new history{ 
					Name = "niets gevonden",
					omschrijving = ""
				});
			}

			return ListHistory;
		}
	}
}

