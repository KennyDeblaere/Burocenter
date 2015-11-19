using System;

using Xamarin.Forms;
using CorflowMobile.Superclasses;
using CorflowMobile.Views;

namespace CorflowMobile
{
	public class ListPrestaties : ContentPage
	{
		private ListView lstvwUsedArticles;
		private StackLayout layout;

		private SyncItems syncItems;

		public ListPrestaties (int prestatieID)
		{
			syncItems = new SyncItems();

			Title = "Verbruikte Artikels";
			Padding = new Thickness (10, 10, 10, 10);
			BackgroundColor = Color.White;

			layout = new StackLayout ();

			lstvwUsedArticles = new ListView {
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate (typeof(PerformanceCell)),
				SeparatorColor = Color.FromHex ("ddd"),
				BackgroundColor = Color.White,
				ItemsSource = PerformanceData.GetData(prestatieID),

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


