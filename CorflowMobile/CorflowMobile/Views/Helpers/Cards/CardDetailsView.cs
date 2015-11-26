using System;
using System.Collections.Generic;
using CorflowMobile.Models;
using Xamarin.Forms;
using CorflowMobile.Data;
using CorflowMobile.Controllers;

namespace CorflowMobile.Views
{
	public class CardDetailsView : ContentView
	{
        private List<Bedrijf> bedrijven; 
		public CardDetailsView (Opdracht opdracht)
		{
            bedrijven = DataController.Instance.GetCompanys();
            BackgroundColor = Color.White;

			Label TitleText = new Label () {
				FormattedText = bedrijven[opdracht.BedrijfID].Naam,
				FontSize = 18,
				TextColor = StyleKit.LightTextColor
			};

			Label DescriptionText = new Label () {
				FormattedText = opdracht.Omschrijving,
				FontSize = 12,
				TextColor = StyleKit.LightTextColor
			};

			var stack = new StackLayout () {
				Spacing = 0,
				Padding = new Thickness (10, 0, 0, 0),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Children = {
					TitleText,
					DescriptionText//,
					//new DateTimeView (opdracht)
				}
			};

			Content = stack;
		}
	}
}