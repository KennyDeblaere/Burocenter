using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace CorflowMobile
{
	public class historyCell : ViewCell
	{
		public historyCell ()
		{
			var NameLabel = new Label()
			{
				FontFamily = "HelveticaNeue-Medium",
				FontSize = 18,
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			NameLabel.SetBinding(Label.TextProperty, "Name");

			var DescriptionLabel = new Label()
			{
				FontAttributes = FontAttributes.Bold,
				FontSize = 18,
				TextColor = Color.FromHex("#666"),

			};
			DescriptionLabel.SetBinding(Label.TextProperty, "omschrijving");


			var cellLayout = new StackLayout
			{
				Spacing = 0,
				Padding = new Thickness(10, 5, 10, 5),
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { NameLabel,DescriptionLabel},
			};

			this.View = cellLayout;
		}
	}
}

