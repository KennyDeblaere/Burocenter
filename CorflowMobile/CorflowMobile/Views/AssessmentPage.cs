using CorflowMobile.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using CorflowMobile.Superclasses;
using System;
using CorflowMobile.Controllers;
using System.Globalization;

namespace CorflowMobile.Views
{
    public class AssessmentPage : ContentPage
	{
		private StackLayout cardLayout;
		private StackLayout layout;
        private Map map;

        private List<Opdracht> Cards;
        private SyncItems syncItems;

		private void InitMap()
		{
			map = new Map
			{
				IsShowingUser = true,
				HeightRequest = 180,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(Cards[0].Latitude), double.Parse(Cards[0].Longitude)), Distance.FromMiles(3)));
			for (int i = 0; i < Cards.Count; i++)
			{
				var pin = new Pin
				{
					Type = PinType.Place,
					Position = new Position(double.Parse(Cards[i].Latitude), double.Parse(Cards[i].Longitude)),
					Label = Cards[i].Omschrijving,
					Address = syncItems.FormattedAddress(Cards[i].BedrijfID)
				};
				map.Pins.Add(pin);
			}
		}

		public AssessmentPage (DateTime date)
		{
            Title = String.Format("{0} voor {1}",
                    LoginController.Instance.GetCurrentUser.Functie == 2 ? "Afspraken" : "Werkopdrachten",
                    date == DateTime.Today ? "vandaag" : date.ToString("d", new CultureInfo("nl-BE")));

            BackgroundColor = Color.White;
            
            

			Cards = new List<Opdracht> ();
            syncItems = new SyncItems();

			cardLayout = new StackLayout {
				Spacing = 15,
				Padding = new Thickness (10),
				VerticalOptions = LayoutOptions.StartAndExpand,
			};

            FillData(date);
			InitMap();

			layout = new StackLayout () {
				Children = {
					map,
					new ScrollView () {
						Content = cardLayout
					}
				}
			};
		}

        private void FillData(DateTime date)
        {
            cardLayout.Children.Clear();
            Cards.Clear();

            Cards = syncItems.GetAssignmentsForLogedInPersonByDate(date);
            foreach (Opdracht assignment in Cards)
                cardLayout.Children.Add(new CardView(assignment));
        }

		protected override void OnAppearing()
		{

			Content = layout;
			base.OnAppearing();
		}
		protected override void OnDisappearing ()
		{
			Content = null;
			base.OnDisappearing ();
		}
	}
}


