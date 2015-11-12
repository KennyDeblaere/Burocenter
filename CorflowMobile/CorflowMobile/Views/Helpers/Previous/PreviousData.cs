using CorflowMobile.Data;
using CorflowMobile.Models;
using CorflowMobile.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
	public class PreviousData
	{
		public static List<Previous> GetData(Opdracht opdracht)
		{
			List<Previous> ListPrevious = new List<Previous> ();
			List<Opdracht> ListOpdracht = (List<Opdracht>)DependencyService.Get<IDataService>().LoadAll<Opdracht>().Where(t => t.BedrijfID == opdracht.BedrijfID).ToList();
			List<OpdrachtWerknemer> ListOpdrachtWerknemer = (List<OpdrachtWerknemer>)DependencyService.Get<IDataService>().LoadAll<OpdrachtWerknemer>();

			if (ListOpdracht.Count > 0){
				foreach (Opdracht opd in ListOpdracht) {
					foreach (OpdrachtWerknemer opdw in ListOpdrachtWerknemer) {
						if ( opd.ID == opdw.OpdrachtID && LoginController.Instance.GetCurrentUser.ID == opdw.WerknemerID) {
							ListPrevious.Add (new Previous{ 
								Description = opd.Omschrijving,
								Datum = (DateTime )opdw.Datum,
								OpdrachtID = opd.ID
							});
						}
					}
				
				}

			}else
			{
				ListPrevious.Add(new Previous
					{
						Description="nog geen contact geweest",
						Datum = DateTime.Now,
						OpdrachtID = 0
					});
			}

			return ListPrevious;
		
		}
	}
}

