using Xamarin.Forms;
using System.Collections.Generic;
using CorflowMobile.Models;
using System.Linq;
using CorflowMobile.Controllers;

namespace CorflowMobile.Views
{
	public class ContactDetails : ContentPage
	{
		private StackLayout layout;
		private StackLayout telephoneLayout;
		private Label lblTelephone, lblEmail, lblbFunction,lblCompany,lblAddress,lblCollegues,lblSoldAt,lblCompanyName,lblTelephoneNumber;
        
        private string formattedAddress;

        private List<Bedrijf> Companys;
        private List<Persoon> collegues;
		private List<Persoon> allPersons;
        private List<VerkochtArtikel> lstSoldArticles;

        private ListView lstvwCollegues,lstvwSoldArticles;
		
		private Button call;
        
		public ContactDetails (Contact persoon)
		{
			Padding = new Thickness (10, 10, 10, 10);
            
            Companys = DataController.Instance.GetCompanys().Where(t => persoon.Company == t.Naam).ToList();
            allPersons = DataController.Instance.GetPersons();
			collegues = new List<Persoon> ();
			formattedAddress = DataController.Instance.FormattedAddress(0);

			if (Companys.Count > 0) {
                collegues = DataController.Instance.GetAllContactsFromACompany(Companys[0].ID);
				lstSoldArticles = DataController.Instance.GetSoldArticlesByCompany(Companys[0]);
                formattedAddress = DataController.Instance.FormattedAddress(0);
			}

            InitComponents(persoon);
            AddCompontentsToLayout(layout);


        }

		protected override void OnAppearing ()
		{
			
			Content = layout;
			base.OnAppearing ();
		}
		protected override void OnDisappearing ()
		{
			Content = null;
			base.OnDisappearing ();
		}

        private void InitLayouts()
        {
            telephoneLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };
            layout = new StackLayout();
        }
        private void InitListView()
        {
            lstvwCollegues = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(ContactCell)),
                SeparatorColor = Color.FromHex("ddd"),
                BackgroundColor = Color.White,
                ItemsSource = ContactData.GetData(collegues),

            };

            lstvwCollegues.ItemSelected += (sender, e) => {
                if (e.SelectedItem != null)
                    Navigation.PushAsync(new ContactDetails(e.SelectedItem as Contact));

                ((ListView)sender).SelectedItem = null;
            };
        }
        private void InitTabGesture(Contact persoon)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null && persoon.Phone != null || persoon.Phone != "")
                {
                    dialer.Dial(persoon.Phone);
                }
            };
            lblTelephoneNumber.GestureRecognizers.Add(tapGestureRecognizer);
        }
        private void InitLabelsPhoneLayout(Contact persoon)
        {
            lblTelephone = new Label
            {
                Text = "Telefoon: ",
                TextColor = Color.Black
            };

            lblTelephoneNumber = new Label
            {
                Text = persoon.Phone,
                TextColor = Color.Blue
            };
        }
        private void InitTelephoneLayout()
        {
            telephoneLayout.Children.Add(lblTelephone);
            telephoneLayout.Children.Add(lblTelephoneNumber);
        }
        private void InitOtherLabels(Contact persoon)
        {
            lblEmail = new Label
            {
                Text = "email:" + persoon.Email,
                TextColor = Color.Black
            };

            lblbFunction = new Label
            {
                Text = "functie:" + DataController.Instance.GetFunctions()[persoon.Functie].Omschrijving,
                TextColor = Color.Black
            };
            lblCompany = new Label
            {
                Text = "Bedrijf",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            lblAddress = new Label
            {
                Text = "adres: " + formattedAddress,
                TextColor = Color.Black
            };

            lblCollegues = new Label
            {
                Text = "Collegas",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            if (LoginController.Instance.GetCurrentUser.Functie == 2)
            {
                lblSoldAt = new Label
                {
                    Text = "Verkochte Artikels",
                    TextColor = Color.Black,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    FontAttributes = FontAttributes.Bold
                };

                lstvwSoldArticles = new ListView
                {
                    HasUnevenRows = true,
                    ItemTemplate = new DataTemplate(typeof(ArtikelCell)),
                    ItemsSource = ArtikelData.GetData(lstSoldArticles),
                    SeparatorColor = Color.FromHex("ddd"),
                    BackgroundColor = Color.White

                };
            }

            lblCompanyName = new Label
            {
                Text = "naam: " + persoon.Company,
                TextColor = Color.Black
            };
        }
        private void InitComponents(Contact persoon)
        {

            InitLayouts();
            InitListView();
            InitLabelsPhoneLayout(persoon);
            InitTabGesture(persoon);
            Title = persoon.Name;
            InitTelephoneLayout();
            BackgroundColor = Color.White;

            InitOtherLabels(persoon);     
        }
        private void AddCompontentsToLayout(StackLayout layout)
        {
            layout.Children.Add(telephoneLayout);
            layout.Children.Add(lblEmail);
            layout.Children.Add(lblbFunction);
            layout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            layout.Children.Add(lblCompany);
            layout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            layout.Children.Add(lblCompanyName);
            layout.Children.Add(lblAddress);
            layout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            layout.Children.Add(lblCollegues);
            layout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            layout.Children.Add(lstvwCollegues);
            if (LoginController.Instance.GetCurrentUser.Functie == 2)
            {

                layout.Children.Add(new BoxView
                {
                    Color = Color.FromHex("ddd"),
                    HeightRequest = 1,
                    VerticalOptions = LayoutOptions.Fill
                });
                layout.Children.Add(lblSoldAt);
                layout.Children.Add(new BoxView
                {
                    Color = Color.FromHex("ddd"),
                    HeightRequest = 1,
                    VerticalOptions = LayoutOptions.Fill
                });
                layout.Children.Add(lstvwSoldArticles);
            }
        }
	}
}


