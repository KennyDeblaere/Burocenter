using CorflowMobile.Controllers;
using CorflowMobile.Models;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
	public class ListSoldArticles : ContentPage
	{
        private ListView lstvwUsedArticles;
		private StackLayout layout;
	

		public ListSoldArticles (Opdracht assignment)
		{
			Title = "Verbruikte Artikels";
			Padding = new Thickness (10, 10, 10, 10);
			BackgroundColor = Color.White;

			layout = new StackLayout ();

			lstvwUsedArticles = new ListView {
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate (typeof(VerbruiktArtikelCell)),
				SeparatorColor = Color.FromHex ("ddd"),
				BackgroundColor = Color.White,
				ItemsSource = VerbruiktDataArtikel.GetData(DataController.Instance.GetUsedArticlesByAssignment(assignment)),

			};

			layout.Children.Add (lstvwUsedArticles);
		}
			
		protected override void OnAppearing ()
		{
			Content = layout;
			base.OnAppearing ();
		}
		protected override void OnDisappearing ()
		{
			Content = null;
			base.OnDisappearing ();
		}
	}
}


