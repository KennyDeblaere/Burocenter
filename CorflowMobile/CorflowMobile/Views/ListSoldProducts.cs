using CorflowMobile.Models;
using CorflowMobile.Superclasses;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
    class ListSoldProducts : ContentPage
    {
        private ListView lstvwSoldArticles;
        private StackLayout layout;

        private SyncItems syncItems;

        public ListSoldProducts(Opdracht opdracht)
        {
            syncItems = new SyncItems();

            Title = "Gekochte Artikels";
            Padding = new Thickness(10, 10, 10, 10);
            BackgroundColor = Color.White;

            layout = new StackLayout();

            lstvwSoldArticles = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(ArtikelCell)),
                ItemsSource = ArtikelData.GetData(syncItems.GetSoldArticlesByCompany(syncItems.GetCompanyFromAssessment(opdracht))),
                SeparatorColor = Color.FromHex("ddd"),
                BackgroundColor = Color.White,
                HeightRequest = 140

            };


            layout.Children.Add(lstvwSoldArticles);
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