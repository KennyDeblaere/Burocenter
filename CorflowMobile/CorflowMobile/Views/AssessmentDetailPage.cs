using System;
using System.Collections.Generic;
using System.Linq;
using CorflowMobile.Models;
using Xamarin.Forms;
using CorflowMobile.Data;
using CorflowMobile.Controllers;
using CorflowMobile.Superclasses;

namespace CorflowMobile.Views
{
	public class AssessmentDetailPage : ContentPage
	{
		StackLayout generalTermsLayout,articleLayout,reportLayout,phoneLayout,articlesButtonLayout,finishedLayout,achievementLayout,achievementsortLayout,timeLayout;
		Label lblCustommerName,lblDescription,lblAddress,lblPhone,lblGeneralTerms,lblPhoneNumber,lblArticle,lblReport,lblFinished,lblAchievement,lblSort,lblDuration,lblMinutes;
		Button btnAddProduct,btnShowArticles, btnAddReport,btnSendAssesment;
        Switch isFinished;
        Picker pckAchievementsort;
        
		private List<Ligplaats> ligplaatsen;
        private List<Prestatie> prestatielijst;

        private SyncItems syncItems;

        private Persoon contactpersoon;
		private IScanner scanner;
		private string formattedAddress;
		
		



		public AssessmentDetailPage (Opdracht opdracht)
		{
            syncItems = new SyncItems();
            
            contactpersoon = (Persoon)DependencyService.Get<IDataService>().LoadAll<Persoon>()
                [DependencyService.Get<IDataService>().LoadAll<BedrijfsPersoon>().
                    Where(t => t.BedrijfID == opdracht.BedrijfID).ToList().Count > 0 ? 
                    DependencyService.Get<IDataService>().LoadAll<BedrijfsPersoon>().Where(t => t.BedrijfID == opdracht.BedrijfID).ToList()[0].PersoonID -1 : 0] ;
            formattedAddress = syncItems.FormattedAddress(opdracht.Adres);
			prestatielijst = syncItems.GetAchievementsFromAssessment (opdracht);
			


			if (prestatielijst.Count == 0) {
				Prestatie presatie = new Prestatie();
				presatie.Aanvang = DateTime.Now;
				presatie.Duur = 0;
				presatie.OpdrachtID = opdracht.ID;
				presatie.PrestatiesoortID = 0;
				DependencyService.Get<IDataService>().Insert(presatie);
				SyncController.Instance.TrySync();
				prestatielijst.Add (presatie);
			}


			Title = "Opdracht: " + opdracht.ID;
			Padding = new Thickness (10, 10, 10, 10);
			scanner = DependencyService.Get<IScanner> ();

			BackgroundColor = Color.White;
			generalTermsLayout = new StackLayout ();
			articleLayout = new StackLayout ();
			reportLayout = new StackLayout ();

			lblGeneralTerms = new Label {
				Text = "Algemene info",
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};

			lblCustommerName = new Label {
				Text = "Naam: "+syncItems.GetCompanys()[opdracht.BedrijfID].Naam,
				TextColor = Color.Black
			};

			lblDescription = new Label {
				Text = "Omschrijving: "+opdracht.Omschrijving,
				TextColor = Color.Black
			};
			lblAddress = new Label{
				Text = "Adres: " + formattedAddress,
				TextColor = Color.Black
			};

			phoneLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal
			};

			lblPhone = new Label {
				Text = "Telefoon: ",
				TextColor = Color.Black
			};

			lblPhoneNumber = new Label {
				Text = contactpersoon.TelefoonWerk,
				TextColor = Color.Blue
			};

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += (s, e) => {
				var dialer = DependencyService.Get<IDialer> ();
				if (dialer != null && contactpersoon.TelefoonWerk != null || contactpersoon.TelefoonWerk!= "") {
					dialer.Dial (contactpersoon.TelefoonWerk);
				}
			};
			lblPhoneNumber.GestureRecognizers.Add(tapGestureRecognizer);

			phoneLayout.Children.Add (lblPhone);
			phoneLayout.Children.Add (lblPhoneNumber);

			generalTermsLayout.Children.Add (lblGeneralTerms);
			generalTermsLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});
			generalTermsLayout.Children.Add (lblCustommerName);
			generalTermsLayout.Children.Add (lblDescription);
			generalTermsLayout.Children.Add (lblAddress);
			generalTermsLayout.Children.Add (phoneLayout);
			generalTermsLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});


			achievementLayout = new StackLayout ();

			lblAchievement = new Label{
				Text = "Prestatie",
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};

			timeLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
			};

			lblDuration = new Label {
				Text = "Duur: ",
				TextColor = Color.Black
			};

			lblMinutes = new Label {
				Text = prestatielijst[0].Duur + " minuten",
				TextColor = Color.Black
			};

			timeLayout.Children.Add (lblDuration);
			timeLayout.Children.Add (lblMinutes);

			achievementsortLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal
			};

			lblSort = new Label{
				Text="soort: ",
				TextColor = Color.Black,
			};

			pckAchievementsort = new Picker
			{
				Title = "Prestatiesoort",
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};
				
			foreach (Prestatiesoort actType in syncItems.GetActivityType().OrderBy(t=>t.ID))
			{
				pckAchievementsort.Items.Add(actType.Omschrijving);
			}

			pckAchievementsort.SelectedIndex = prestatielijst[0].PrestatiesoortID;
				
			achievementsortLayout.Children.Add (lblSort);
			achievementsortLayout.Children.Add (pckAchievementsort);

			achievementLayout.Children.Add (lblAchievement);
			achievementLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});
			achievementLayout.Children.Add (timeLayout);
			achievementLayout.Children.Add (achievementsortLayout);
			achievementLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});
					
			lblArticle = new Label {
				Text = "Artikel",
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};

			articlesButtonLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal
			};
			btnAddProduct = new Button {
				Text = "voeg product toe"	
			};

			if (opdracht.Afgewerkt == true) {
				btnAddProduct.IsEnabled = false;
			};

			btnShowArticles = new Button {
				Text = "verbruikte producten"	
			};
			articlesButtonLayout.Children.Add (btnAddProduct);
			articlesButtonLayout.Children.Add (btnShowArticles);

			articleLayout.Children.Add (lblArticle);
			articleLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});
			articleLayout.Children.Add (articlesButtonLayout);
			articleLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});

			lblReport = new Label {
				Text = "Verslag",
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};
			btnAddReport = new Button {
				Text = "voeg verslag toe"
			};

			reportLayout.Children.Add (lblReport);
			reportLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});
			reportLayout.Children.Add (btnAddReport);
			reportLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});


			finishedLayout = new StackLayout ();

			lblFinished = new Label {
				Text = "Afgewerkt",
				TextColor = Color.Black,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};

			isFinished = new Switch {
				IsToggled =  (bool) opdracht.Afgewerkt
			};

			finishedLayout.Children.Add (lblFinished);
			finishedLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});
			finishedLayout.Children.Add (isFinished);
			finishedLayout.Children.Add (new BoxView{
				Color = Color.FromHex ("ddd"),
				HeightRequest=1,
				VerticalOptions = LayoutOptions.Fill
			});

			btnSendAssesment = new Button {
				Text="Verzend opdracht"	
			};
					
			btnAddProduct.Clicked += async (object sender, EventArgs e) => {
				var foundScan =  await scanner.Scan();
				System.Diagnostics.Debug.WriteLine("gevonden: " +foundScan.Text);

				string[] foundScanString = foundScan.Text.Split('/');
				System.Diagnostics.Debug.WriteLine("deel 1: " +foundScanString[0]);
				System.Diagnostics.Debug.WriteLine("deel 2: " +foundScanString[1]);

				foreach(Artikel art in syncItems.GetArticles()){
					if(art.Barcode == foundScanString[0])
					{
						System.Diagnostics.Debug.WriteLine("juiste artikel ingescand :D :" + art.Omschrijving);
						bool found = false;
						foreach(Verbruiksartikel usedArticle in syncItems.GetUsedArticles())
						{
							if(usedArticle.ArtikelID == art.ID && usedArticle.OpdrachtID == opdracht.ID)
							{
								found = true;
								usedArticle.Gebruikt++;
								DependencyService.Get<IDataService>().Update(usedArticle);
							}
						}

						if (!found) {
							Verbruiksartikel usedArt = new Verbruiksartikel ();
							usedArt.ArtikelID = art.ID;
							usedArt.Gebruikt = 1;
							usedArt.OpdrachtID = opdracht.ID;
							DependencyService.Get<IDataService>().Insert(usedArt);
						}

						//toevoegen aangekocht product
						VerkochtArtikel sold = new VerkochtArtikel();
						sold.Artikel = art.ID;
						sold.Bedrijf = syncItems.GetCompanys()[opdracht.BedrijfID].ID;
						sold.Serienummer = foundScanString[1];
						sold.Datum = DateTime.Today;
						DependencyService.Get<IDataService>().Insert(sold);

                        

						foreach(Ligplaats place in syncItems.GetStockPlaces())
						{
							if(place.VerantwoordelijkeLigplaatsID == LoginController.Instance.GetCurrentUser.ID)
							{
								foreach(VerbruikLigplaats usedPlace in syncItems.GetUsedArticlesByStockPlace(place))
								{
									if(place.ID == usedPlace.LigplaatsID && art.ID == usedPlace.ArtikelID)
									{
										usedPlace.AantalStock--;
										DependencyService.Get<IDataService>().Update(usedPlace);
									}
								}
							}
						}
					}
				}

				SyncController.Instance.TrySync();
			};

			btnShowArticles.Clicked += (object sender, EventArgs e) => {
				Navigation.PushAsync(new ListSoldArticles(opdracht));
			};


			btnAddReport.Clicked += (object sender, EventArgs e) => {
				Navigation.PushAsync(new ReportPage(prestatielijst[0]));
			};

			btnSendAssesment.Clicked += (object sender, EventArgs e) => {
				if(isFinished.IsToggled == true && pckAchievementsort.SelectedIndex != -1 ){
					opdracht.Afgewerkt = true;
					opdracht.Statuslabel = "Afgewerkt";

					switch (pckAchievementsort.Items[pckAchievementsort.SelectedIndex]) 
					{
						case "Herstelling" : prestatielijst[0].PrestatiesoortID = 1;
							break;
						case "Verkoop" : prestatielijst[0].PrestatiesoortID = 2;
							break;
						case "Onderhoud" : prestatielijst[0].PrestatiesoortID = 0;
							break; 
					}

					DependencyService.Get<IDataService> ().Update(prestatielijst[0]);
					DependencyService.Get<IDataService>().Update(opdracht);
					SyncController.Instance.TrySync();
					Navigation.PopAsync ();
				}else{
					DisplayAlert("Error", "Gelieve een prestatiesoort te selcteren. of het product afgewerkt aan te vinken", "Ok");
				}
			};


		
		}
			
		protected override void OnAppearing()
		{

			Content =new ScrollView{ Content = new StackLayout {
                Children = {
                    generalTermsLayout,
                    achievementLayout,
                    articleLayout,
                    reportLayout,
                    finishedLayout,
                    btnSendAssesment
					}
				}
            };

			base.OnAppearing();
		}

		protected override void OnDisappearing ()
		{
			Content = null;
			base.OnDisappearing ();
		}
	}
}


