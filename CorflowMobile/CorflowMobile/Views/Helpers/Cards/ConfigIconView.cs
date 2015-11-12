using CorflowMobile.Models;
using Xamarin.Forms;
using CorflowMobile.Controllers;
using CorflowMobile.Views;

namespace CorflowMobile.Views
{
    public class ConfigIconView : ContentView
	{
        public ConfigIconView(Opdracht opdracht)
        {
            BackgroundColor = StyleKit.CardFooterBackgroundColor;
            Image image = new Image
            {
                HeightRequest = 10,
                WidthRequest = 10,
                Source = StyleKit.Icons.Cog
            };

            Label l = new Label
            {
                Text = "Details",
                FontSize = 9,
                FontAttributes = FontAttributes.Bold,
                TextColor = StyleKit.LightTextColor
            };
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) => {
                if (LoginController.Instance.GetCurrentUser.Functie == 2)
                {
                    Navigation.PushAsync(new AppointmentDetailPage(opdracht));
                }
                else
                {
                    Navigation.PushAsync(new AssessmentDetailPage(opdracht));
                }

            };
            image.GestureRecognizers.Add(tapGestureRecognizer);
            l.GestureRecognizers.Add(tapGestureRecognizer);
            Content = new StackLayout
            {
                Padding = new Thickness(5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    image, l
                }
            };
        }
    }
}