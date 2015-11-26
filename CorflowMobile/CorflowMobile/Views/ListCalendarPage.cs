using System;
using CorflowMobile.Controllers;
using Xamarin.Forms;
using System.Globalization;
using CorflowMobile.Models;
using System.Collections.Generic;
using CorflowMobile.Data;
using System.Linq;
using CorflowMobile.Views;


namespace CorflowMobile
{
	public class ListCalendarPage : ContentPage
	{
		private StackLayout layout;
		private ListView lstvCalendar;
		private List<Opdracht> lijstgevonden;

		public ListCalendarPage (DateTime date)
		{
			Title = String.Format("{0} voor {1}",
				LoginController.Instance.GetCurrentUser.Functie == 2 ? "Afspraken" : "Werkopdrachten",
				date == DateTime.Today ? "vandaag" : date.ToString("d", new CultureInfo("nl-BE")));;

			layout = new StackLayout();

			lijstgevonden = DataController.Instance.GetAssignmentsForLogedInPersonByDate(date);

			lstvCalendar = new ListView
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(historyCell)),
				ItemsSource = historyData.GetData(lijstgevonden),
				SeparatorColor = Color.FromHex("ddd"),
				BackgroundColor = Color.White,
			};


			lstvCalendar.ItemSelected += (sender, e) => {
				if (e.SelectedItem != null){
					history his = e.SelectedItem as history;
					List<Opdracht> lijstopdracht = DataController.Instance.GetAssessmentsByID(his.opdrachtID);
					Navigation.PushAsync(new AppointmentDetailPage(lijstopdracht[0]));

				}

				((ListView)sender).SelectedItem = null;

			};

			layout.Children.Add(lstvCalendar);
		}
		protected override void OnAppearing()
		{
			Content = layout;
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			Content = null;
			base.OnDisappearing();
		}
	}
}


