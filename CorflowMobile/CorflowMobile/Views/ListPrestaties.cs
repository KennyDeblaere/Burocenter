using System;

using Xamarin.Forms;

namespace CorflowMobile.Views
{
    public class ListPrestaties : ContentPage
    {
        private ListView lstvwUsedArticles;
        private StackLayout layout;

        public ListPrestaties(int prestatieID)
        {
            Title = "Verbruikte Artikels";
            Padding = new Thickness(10, 10, 10, 10);
            BackgroundColor = Color.White;

            layout = new StackLayout();

            lstvwUsedArticles = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(PerformanceCell)),
                SeparatorColor = Color.FromHex("ddd"),
                BackgroundColor = Color.White,
                ItemsSource = PerformanceData.GetData(prestatieID),

            };

            layout.Children.Add(lstvwUsedArticles);
        }

        protected override void OnAppearing()
        {
            Content = layout;
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            Content = null;
            base.OnDisappearing();
        }
    }
}

