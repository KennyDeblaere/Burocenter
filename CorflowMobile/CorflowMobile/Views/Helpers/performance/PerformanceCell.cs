using System;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
    public class PerformanceCell : ViewCell
    {
        public PerformanceCell()
        {
            var NameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = 18,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            NameLabel.SetBinding(Label.TextProperty, "Name");

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { NameLabel },
            };

            this.View = cellLayout;
        }
    }
}
