using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CorflowMobile.Views
{
    public class ContactCell : ViewCell
    {
        public ContactCell()
        {
            var contactProfileImage = new CircleImage
            {
                BorderColor = Color.FromHex("#BDC3C7"),
                BorderThickness = 3,
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            contactProfileImage.SetBinding(Image.SourceProperty, "ImageSource");

            var nameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = 18,
                TextColor = Color.Black
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            var companyLabel = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                TextColor = Color.FromHex("#666")
            };
            companyLabel.SetBinding(Label.TextProperty, new Binding("Company", stringFormat: "{0}"));

            var emailLabel = new Label()
            {
                FontSize = 12
            };
            emailLabel.SetBinding(Label.TextProperty, "Email");

            var phoneLabel = new Label()
            {
                FontSize = 12,
                TextColor = Color.Gray
            };
            phoneLabel.SetBinding(Label.TextProperty, "Phone");


            var phoneStack = new StackLayout()
            {
                Spacing = 3,
                Orientation = StackOrientation.Horizontal,
                Children = { phoneLabel }
            };

            var statusLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { companyLabel, emailLabel }
            };

            var detailLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { nameLabel, statusLayout, phoneStack }
            };

            var tapImage = new Image()
            {
                Source = "tap.png",
                HorizontalOptions = LayoutOptions.End,
                HeightRequest = 12,
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { contactProfileImage, detailLayout, tapImage },
            };

            this.View = cellLayout;
        }
    }
}
