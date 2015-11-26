using CorflowMobile.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
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

		private void InitMap()
		{
			map = new Map
			{
				IsShowingUser = true,
				HeightRequest = 180,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
            if (Cards.Count > 0)
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(Cards[0].Latitude, new CultureInfo("en-US")), double.Parse(Cards[0].Longitude, new CultureInfo("en-US"))), Distance.FromMiles(3)));
                for (int i = 0; i < Cards.Count; i++)
                {
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(double.Parse(Cards[i].Latitude, new CultureInfo("en-US")), double.Parse(Cards[i].Longitude, new CultureInfo("en-US"))),
                        Label = Cards[i].Omschrijving,
                        Address = DataController.Instance.FormattedAddress(Cards[i].BedrijfID)
                    };
                    map.Pins.Add(pin);
                }
            }
            else
            {
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(51.21335, 3.19485), Distance.FromMiles(3)));
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(51.21335, 3.19485),
                    Label = "Buro Center",
                    Address = DataController.Instance.FormattedAddress(103)
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

            Cards = DataController.Instance.GetAssignmentsForLogedInPersonByDate(date);
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


