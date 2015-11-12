using System;
using Xamarin.Forms;
using CorflowMobile.Models;

namespace CorflowMobile.Views
{
	public class CardStatusView : ContentView
	{
		public CardStatusView (Opdracht opdracht)
		{
			var statusBoxView = new BoxView {
				VerticalOptions = LayoutOptions.Fill,
				HorizontalOptions = LayoutOptions.Fill
			};

            if(opdracht.Afgewerkt == true)
                statusBoxView.BackgroundColor = StyleKit.Status.CompletedColor;
            else
                statusBoxView.BackgroundColor = StyleKit.Status.UnresolvedColor;

			Content = statusBoxView;
		}
	}
}