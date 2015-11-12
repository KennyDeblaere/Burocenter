using System.Collections.Generic;
using CorflowMobile.Models;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
    class ListContacts : ContentPage
    {
        private ListView lstvwContacts;
        private StackLayout layout;

        public ListContacts(List<Persoon> Contactpersonen)
        {
            Title = "Contactpersonen";
            Padding = new Thickness(10, 10, 10, 10);
            BackgroundColor = Color.White;

            layout = new StackLayout();

            lstvwContacts = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(ContactCell)),
                SeparatorColor = Color.FromHex("ddd"),
                BackgroundColor = Color.White,
                ItemsSource = ContactData.GetData(Contactpersonen),
                HeightRequest = 140
            };

            lstvwContacts.ItemSelected += (sender, e) => {
                if (e.SelectedItem != null)
                    Navigation.PushAsync(new ContactDetails(e.SelectedItem as Contact));

                ((ListView)sender).SelectedItem = null;
            };

            layout.Children.Add(lstvwContacts);
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