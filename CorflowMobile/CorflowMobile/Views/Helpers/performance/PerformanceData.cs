using System;

using CorflowMobile.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using CorflowMobile.Data;
using System.Linq;

namespace CorflowMobile
{
	public class PerformanceData
	{
		public static List<Performance> GetData(int prestatieID)
		{
			List<PrestatiesPrestatiesoorten> ListPrestatiesPrestatiesoort = (List<PrestatiesPrestatiesoorten>)DependencyService.Get<IDataService> ().LoadAll<PrestatiesPrestatiesoorten> ().Where(t => t.PrestatieID == prestatieID).ToList();
			List<Prestatiesoort> ListPrestatieSoort = (List<Prestatiesoort>)DependencyService.Get<IDataService>().LoadAll<Prestatiesoort>();
			List<Performance> ListPerformance = new List<Performance> ();


			if (ListPrestatiesPrestatiesoort.Count > 0) {
				foreach (PrestatiesPrestatiesoorten pres in ListPrestatiesPrestatiesoort) {
					foreach (Prestatiesoort soort in ListPrestatieSoort) {
						if (pres.PrestatieSoortID == soort.ID) {
							Performance per = new Performance {
								Name = soort.Omschrijving	
							};
							ListPerformance.Add (per);
						}
					}
				}
			} else {
				ListPerformance.Add (new Performance {
					Name = "Nog Geen Perfomance"
				});
			}

			return ListPerformance;

		}
	}
}

