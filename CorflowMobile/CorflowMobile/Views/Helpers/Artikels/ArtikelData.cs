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
	public class ArtikelData
	{
		public static List<GekochtArtikel> GetData(List<VerkochtArtikel> verkochteArtikels)
		{
			List<GekochtArtikel> gekochtArtikels = new List<GekochtArtikel> ();

			if (verkochteArtikels != null) {
				List<Artikel> artikelen = (List<Artikel>)DependencyService.Get<IDataService>().LoadAll<Artikel>();

				foreach (Artikel art in artikelen) {

					foreach(VerkochtArtikel ver in verkochteArtikels){
						if (ver.Artikel == art.ID) {
							gekochtArtikels.Add (new GekochtArtikel{
								Serienumber = ver.Serienummer,
								gekochtdatum = ver.Datum,
								Name = art.Omschrijving
							});
						
						}
					}
				}
					
			}else
			{
				gekochtArtikels.Add(new GekochtArtikel
					{
						Serienumber="",
						Name="Nog niets gekocht",
						gekochtdatum = DateTime.Now

					});
			}
			return gekochtArtikels;

		}
	}
}

