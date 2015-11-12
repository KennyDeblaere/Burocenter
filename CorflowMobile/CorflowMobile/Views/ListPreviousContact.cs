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
	public class ListPreviousContact : ContentPage
	{
		private StackLayout layout;
		private ListView lstvPreviousContact;

		public ListPreviousContact (Opdracht opdracht,String CompanyName)
		{
			Title = CompanyName;
			Padding = new Thickness(10, 10, 10, 10);
			BackgroundColor = Color.White;

			layout = new StackLayout();

			lstvPreviousContact = new ListView
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(PreviousCell)),
				ItemsSource = PreviousData.GetData(opdracht),
				SeparatorColor = Color.FromHex("ddd"),
				BackgroundColor = Color.White,
			};

			lstvPreviousContact.ItemSelected += (sender, e) => {
				if (e.SelectedItem != null){
					Previous pre = e.SelectedItem as Previous;
					List<Opdracht> lijstopdracht = (List<Opdracht>)DependencyService.Get<IDataService>().LoadAll<Opdracht>().Where(t => t.ID == pre.OpdrachtID).ToList();
					Navigation.PushAsync(new AssessmentDetailPage(lijstopdracht[0]));

				}
					
				((ListView)sender).SelectedItem = null;

			};


			layout.Children.Add(lstvPreviousContact);
			
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


