using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
	public class VerbruiktArtikelCell : ViewCell
	{
		public VerbruiktArtikelCell ()
		{
			var nameLabel = new Label()
			{
				FontFamily = "HelveticaNeue-Medium",
				FontSize = 18,
				TextColor = Color.Black
			};
			nameLabel.SetBinding(Label.TextProperty, "Name");

			var aantalLabel = new Label()
			{
				FontAttributes = FontAttributes.Bold,
				FontSize = 18,
				TextColor = Color.Black
			};
			aantalLabel.SetBinding(Label.TextProperty, "aantal");

			var cellLayout = new StackLayout
			{
				Padding = new Thickness(10, 5, 10, 5),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { nameLabel, aantalLabel },
			};

			this.View = cellLayout;
		}
	}
}

