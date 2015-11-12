using CorflowMobile.Data;
using CorflowMobile.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using CorflowMobile.Superclasses;

namespace CorflowMobile.Views
{
	public class ContactsPage : ContentPage
	{
        private Picker picker;
        private SearchBar searchBar;
        private ListView lstvwContacts;
        private StackLayout searchLayout;
        private Image options;

        private ObservableCollection<Persoon> persons;
        private List<Persoon> keepPersons;

        private SyncItems syncItems;

        int selectedIndex = 0;

        public ObservableCollection<Persoon> Persons
        {
            get { return persons; }
            set { persons = value; OnPropertyChanged("Personen"); }
        }

        
        public ContactsPage ()
		{

            persons = new ObservableCollection<Persoon>();
            keepPersons = (List<Persoon>)DependencyService.Get<IDataService>().LoadAll<Persoon>();

            syncItems = new SyncItems();

            InitSearchBar();
            InitPicker();
            InitSearchLayout();
            InitLstvwContacts();

			var layout = new StackLayout {
				Children = {
					searchLayout,
					lstvwContacts
				}
			};

			Content = layout;
			Title = "Search for contacts";

			BackgroundColor = Color.FromHex ("333333");
		}

        private void FilterPersonen(string text)
        {
            Persons.Clear();
            for (int i = 0; i < getFilteredPersons(picker.SelectedIndex,text).Count; i++)
                Persons.Add(getFilteredPersons(picker.SelectedIndex, text)[i]);
            keepPersons = Persons.ToList();
            lstvwContacts.ItemsSource = ContactData.GetData(keepPersons);
        }
        private List<Persoon> getFilteredPersons(int index, string text)
        {
            List<Persoon> tmpPersons = syncItems.GetContactPersons().Where(t => t.Naam.ToLower().Contains(text.ToLower())).ToList();
            switch (index)
            {
                case 1:
                    tmpPersons = syncItems.GetContactPersons().Where(t => t.Voornaam.ToLower().Contains(text.ToLower())).ToList();
                    break;
                case 2:
                    searchBar.Text += "Bedrijf";
                    List<Bedrijf> companys = syncItems.GetCompanys().Where(t => t.Naam.ToLower().Contains(text.ToLower())).ToList();
                    List<BedrijfsPersoon> companyContacts = (List<BedrijfsPersoon>)DependencyService.Get<IDataService>().LoadAll<BedrijfsPersoon>().ToList();
                    List<BedrijfsPersoon> tmpBp = new List<BedrijfsPersoon>();
                    for (int i = 0; i < companys.Count; i++)
                    {
                        for (int j = 0; j < companyContacts.Count; j++)
                        {
                            if (companys[i].ID == companyContacts[j].BedrijfID)
                                tmpBp.Add(companyContacts[j]);
                        }
                    }
                    foreach (BedrijfsPersoon bp in tmpBp)
                        tmpPersons.Add(syncItems.GetContactPersons()[bp.PersoonID - 1]);
                    break;
                case 3:
                    tmpPersons = tmpPersons = syncItems.GetContactPersons().Where(t => t.Email.ToLower().Contains(text.ToLower())).ToList();
                    break;
                case 4:
                    tmpPersons = tmpPersons = syncItems.GetContactPersons().Where(t => t.TelefoonWerk.ToLower().Contains(text.ToLower())).ToList();
                    break;
                case 5:
                    tmpPersons = tmpPersons = syncItems.GetContactPersons().Where(t => t.Fax.ToLower().Contains(text.ToLower())).ToList();
                    break;
                default:
                    tmpPersons = syncItems.GetContactPersons().Where(t => t.Naam.ToLower().Contains(text.ToLower())).ToList();
                    break;
            }
            return tmpPersons;
        }
        private void options_OnClick(View v, Object o) {
			picker.Focus();
		}


        private void InitSearchBar()
        {
            searchBar = new SearchBar
            {
                Placeholder = "Zoek contacten",
                CancelButtonColor = Color.FromHex("4c4c4c"),
            };

            searchBar.TextChanged += (sender, e) => FilterPersonen(searchBar.Text);
        }
        private void InitPicker()
        {
            options = new Image
            {
                Source = "Config.png",
                WidthRequest = 22
            };

            picker = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = false
            };

            picker.Items.Add("Naam");
            picker.Items.Add("Voornaam");
            picker.Items.Add("Bedrijf");
            picker.Items.Add("E-mail");
            picker.Items.Add("Telefoon");
            picker.Items.Add("Fax");

            options.GestureRecognizers.Add(new TapGestureRecognizer(options_OnClick));
        }
        private void InitSearchLayout()
        {
            searchLayout = new StackLayout
            {
                BackgroundColor = Color.FromHex("333333"),
                Orientation = StackOrientation.Horizontal,
                Children = {
                    searchBar,
                    options,
                    picker
                },
            };
        }
        private void InitLstvwContacts()
        {
            lstvwContacts = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(ContactCell)),
                ItemsSource = ContactData.GetData(syncItems.GetContactPersons()),
                SeparatorColor = Color.FromHex("ddd"),
                BackgroundColor = Color.White

            };

            lstvwContacts.ItemSelected += (sender, e) => {
                if (e.SelectedItem != null)
                    Navigation.PushAsync(new ContactDetails(e.SelectedItem as Contact));

                ((ListView)sender).SelectedItem = null;
            };
        }
    }
}


