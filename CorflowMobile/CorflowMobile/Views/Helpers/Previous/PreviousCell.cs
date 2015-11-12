using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
	public class PreviousCell : ViewCell
	{
		public PreviousCell ()
		{
			var DescriptionLabel = new Label()
			{
				FontFamily = "HelveticaNeue-Medium",
				FontSize = 18,
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			DescriptionLabel.SetBinding(Label.TextProperty, "Description");

			var DatumLabel = new Label()
			{
				FontAttributes = FontAttributes.Bold,
				FontSize = 18,
				TextColor = Color.FromHex("#666"),

			};
			DatumLabel.SetBinding(Label.TextProperty, "Datum");


			var cellLayout = new StackLayout
			{
				Spacing = 0,
				Padding = new Thickness(10, 5, 10, 5),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { DescriptionLabel, DatumLabel},
			};

			this.View = cellLayout;
		}


	}
}

