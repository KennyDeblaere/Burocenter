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
	public class VerbruiktDataArtikel
	{
		public static List<ListVerbruiktArtikel> GetData(List<Verbruiksartikel> verbruikArtikels)
		{
			List<ListVerbruiktArtikel> ListVbart = new List<ListVerbruiktArtikel> ();

			if (verbruikArtikels.Count > 0) {
				List<Artikel> artikelen = DataController.Instance.GetArticles();

				foreach (Verbruiksartikel vbart in verbruikArtikels) {

					foreach(Artikel art in artikelen){
						if (vbart.ArtikelID == art.ID) {
							ListVbart.Add (new ListVerbruiktArtikel{ 
								Name = art.Omschrijving,
								aantal = vbart.Gebruikt
							});
						}
					}
				}

			}else
			{
				ListVbart.Add(new ListVerbruiktArtikel
					{
						Name="Nog niets verbruikt",
						aantal = 0
					});
			}

			return ListVbart;

		}
	}
}

