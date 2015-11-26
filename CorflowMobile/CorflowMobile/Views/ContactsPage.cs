using CorflowMobile.Data;
using CorflowMobile.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using CorflowMobile.Controllers;

namespace CorflowMobile.Views
{
	public class ContactsPage : ContentPage
	{
		private List<Persoon> lstContactPersons = DataController.Instance.GetContactPersons();
		private List<Bedrijf> lstCompanys = DataController.Instance.GetCompanys();
		private List<BedrijfsPersoon> lstCompanyPersons = DataController.Instance.GetCompanyPersons();

		private Picker picker;
		private SearchBar searchBar;
		private ListView lstvwContacts;
		private StackLayout searchLayout;
		private Image options;

		private ObservableCollection<Persoon> persons;
		private List<Persoon> keepPersons;

		int selectedIndex = 0;

		public ObservableCollection<Persoon> Persons
		{
			get { return persons; }
			set { persons = value; OnPropertyChanged("Personen"); }
		}


		public ContactsPage ()
		{

			persons = new ObservableCollection<Persoon>();
			keepPersons = DataController.Instance.GetPersons();

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

			lstvwContacts.ItemsSource = ContactData.GetData(getFilteredPersons(picker.SelectedIndex, text));
		}
		private List<Persoon> getFilteredPersons(int index, string text)
		{
			List<Persoon> tmpPersons = DataController.Instance.GetContactPersons();
			List<Persoon> allContactPersons = DataController.Instance.GetContactPersons();
			List<Persoon> allPersons = DataController.Instance.GetPersons();
			List<Bedrijf> allCompanys = DataController.Instance.GetCompanys();
			switch (index)
			{
			case 1:
				tmpPersons = allContactPersons.Where(t => t.Voornaam.ToLower().Contains(text.ToLower())).ToList();
				break;
			case 2:
				List<Bedrijf> companys = allCompanys.Where(t => t.Naam.ToLower().Contains(text.ToLower())).ToList();
				List<BedrijfsPersoon> companyContacts = (List<BedrijfsPersoon>)DependencyService.Get<IDataService>().LoadAll<BedrijfsPersoon>().ToList();
				List<BedrijfsPersoon> tmpBp = new List<BedrijfsPersoon>();
				for (int i = 0; i < companys.Count; i++)
				{
					tmpBp.AddRange(companyContacts.Where(t => t.BedrijfID == companys[i].ID).ToList());
				}
				List<Persoon> newList = new List<Persoon>();
				foreach (BedrijfsPersoon bp in tmpBp)
					if (allPersons[bp.PersoonID - 1].Functie == 0)
						newList.Add(allPersons[bp.PersoonID - 1]);
				if (newList.Count > 0)
					tmpPersons = newList;
				else
					tmpPersons = allContactPersons;
				break;
			case 3:
				tmpPersons = allContactPersons.Where(t => t.Email.ToLower().Contains(text.ToLower())).ToList();
				break;
			case 4:
				tmpPersons = allContactPersons.Where(t => t.TelefoonWerk.ToLower().Contains(text.ToLower())).ToList();
				break;
			case 5:
				tmpPersons = allContactPersons.Where(t => t.Fax.ToLower().Contains(text.ToLower())).ToList();
				break;
			default:
				tmpPersons = allContactPersons.Where(t => t.Naam.ToLower().Contains(text.ToLower())).ToList();
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
				Title = "Kies de zoekterm",
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
				ItemsSource = ContactData.GetData(DataController.Instance.GetContactPersons()),
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


