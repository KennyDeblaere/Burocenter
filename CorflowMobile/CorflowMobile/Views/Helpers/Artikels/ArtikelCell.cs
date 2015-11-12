using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
	public class ArtikelCell : ViewCell
	{
		public ArtikelCell ()
		{
			var nameLabel = new Label()
			{
				FontFamily = "HelveticaNeue-Medium",
				FontSize = 18,
				TextColor = Color.Black
			};
			nameLabel.SetBinding(Label.TextProperty, "Name");

			var serieLabel = new Label()
			{
				FontAttributes = FontAttributes.Bold,
				FontSize = 12,
				TextColor = Color.FromHex("#666")
			};
			serieLabel.SetBinding(Label.TextProperty, "Serienumber");

			var datumLabel = new Label()
			{
				FontSize = 12,
				TextColor = Color.Gray
			};
			datumLabel.SetBinding(Label.TextProperty, "gekochtdatum");

			var cellLayout = new StackLayout
			{
				Spacing = 0,
				Padding = new Thickness(10, 5, 10, 5),
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = { nameLabel, serieLabel, datumLabel },
			};

			this.View = cellLayout;
		}
	}
}

