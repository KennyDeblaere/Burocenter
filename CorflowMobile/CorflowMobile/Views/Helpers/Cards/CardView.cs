using System;
using CorflowMobile.Models;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
	public class CardView : ContentView
	{
		FileImageSource source;

        public CardView (Opdracht opdracht)
		{
			Card card = new Card ();
			Grid grid = new Grid {
				Padding = new Thickness (0, 1, 1, 1),
				RowSpacing = 1,
				ColumnSpacing = 1,		
				BackgroundColor = StyleKit.CardBorderColor,
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (70, GridUnitType.Absolute) },
					new RowDefinition { Height = new GridLength (30, GridUnitType.Absolute) }
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (4, GridUnitType.Absolute) },
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
					new ColumnDefinition { Width = new GridLength (60, GridUnitType.Absolute) }
				}
			};

            // Meegeven als de opdracht is afgewerkt.
			grid.Children.Add (
				new CardStatusView (opdracht), 0, 1, 0, 2);

            // Meegeven van de detail (bedrijf, adres ... )
			grid.Children.Add (new CardDetailsView (opdracht), 1, 4, 0, 1);



			grid.Children.Add (new IconLabelView (opdracht,StyleKit.Icons.Alert, "Navigeer"), 1, 1);


			switch (opdracht.Statuslabel) {

			case "Afgewerkt":
				source = StyleKit.Icons.Completed;
				break;
			case "Start":
				source = StyleKit.Icons.Resume;
				break;
			case "Stop":
				source = StyleKit.Icons.Stop;
				break;

			default:
				break;
			}

			grid.Children.Add (
				new IconLabelView (opdracht,
					source,
					opdracht.Statuslabel
				)
				, 2, 1);

            //Detailpage
			grid.Children.Add (new ConfigIconView (opdracht), 3, 1);

			Content = grid;
		}
	}
}