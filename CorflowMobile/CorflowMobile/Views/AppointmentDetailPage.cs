using CorflowMobile.Controllers;
using CorflowMobile.Data;
using CorflowMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Toasts.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
    public class AppointmentDetailPage : ContentPage
    {
        private StackLayout layout, generalInfoLayout, ContactLayout, articleLayout, reportLayout, finishedLayout, achievementsortLayout, achievementLayout, timeLayout, previousLayout;
        private Label lblGeneralInfo, lblAddress, lblBTW, lblContact, lblSoldArticle, lblReport, lblFinished, lblSort, lblAchievement, lblDuration, lblMinutes, lblPrevious;
        private Button btnAddReport, btnSendAppointment, btnListSoldArticles, btnListContacts, btnListPreviousContact, btnshowListPerformances, btnAddPerformance;
        private Switch isFinished;
        private Picker pckAchievementsort;

        private List<Prestatie> prestatielijst;

        private string formattedAddress;

        public AppointmentDetailPage(Opdracht opdracht)
        {
            formattedAddress = DataController.Instance.FormattedAddress(DataController.Instance.GetCompanyFromAssessment(opdracht).ID);
            prestatielijst = DataController.Instance.GetAchievements();

            Title = DataController.Instance.GetCompanyFromAssessment(opdracht).Naam;
            BackgroundColor = Color.White;
            Padding = new Thickness(10, 10, 10, 10);
            layout = new StackLayout();

            generalInfoLayout = new StackLayout();

            lblGeneralInfo = new Label
            {
                Text = "Algemene info",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            lblAddress = new Label
            {
                Text = "Adres: " + formattedAddress,
                TextColor = Color.Black
            };

            lblBTW = new Label
            {
                Text = "BTW: " + DataController.Instance.GetCompanyFromAssessment(opdracht).BTW,
                TextColor = Color.Black
            };

            generalInfoLayout.Children.Add(lblGeneralInfo);
            generalInfoLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            generalInfoLayout.Children.Add(lblAddress);
            generalInfoLayout.Children.Add(lblBTW);
            generalInfoLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });

            ContactLayout = new StackLayout();

            lblContact = new Label
            {
                Text = "Contactpersonen",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            btnListContacts = new Button
            {
                Text = "Contactpersonen"
            };

            ContactLayout.Children.Add(lblContact);
            ContactLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            ContactLayout.Children.Add(btnListContacts);
            ContactLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });

            articleLayout = new StackLayout();

            lblSoldArticle = new Label
            {
                Text = "Gekochte Artikels",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            btnListSoldArticles = new Button
            {
                Text = "Gekochte Artikels"
            };


            articleLayout.Children.Add(lblSoldArticle);
            articleLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            articleLayout.Children.Add(btnListSoldArticles);
            articleLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });

            reportLayout = new StackLayout();

            lblReport = new Label
            {
                Text = "Verslag",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };
            btnAddReport = new Button
            {
                Text = "voeg verslag toe"
            };

            reportLayout.Children.Add(lblReport);
            reportLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            reportLayout.Children.Add(btnAddReport);
            reportLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });

            previousLayout = new StackLayout();

            lblPrevious = new Label
            {
                Text = "Vorige Contact momenten",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            btnListPreviousContact = new Button
            {
                Text = "Vorige Contact"
            };

            previousLayout.Children.Add(lblPrevious);
            previousLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            previousLayout.Children.Add(btnListPreviousContact);
            previousLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });



            achievementLayout = new StackLayout();

            lblAchievement = new Label
            {
                Text = "Prestatie",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            timeLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            lblDuration = new Label
            {
                Text = "Duur: ",
                TextColor = Color.Black
            };

            lblMinutes = new Label
            {
                Text = prestatielijst[0].Duur + " minuten",
                TextColor = Color.Black
            };

            timeLayout.Children.Add(lblDuration);
            timeLayout.Children.Add(lblMinutes);

            achievementsortLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            lblSort = new Label
            {
                Text = "soort: ",
                TextColor = Color.Black,
            };

            pckAchievementsort = new Picker
            {
                Title = "Prestatiesoort",
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            foreach (Prestatiesoort actType in DataController.Instance.GetAchievementtypes().OrderBy(t => t.ID))
            {
                pckAchievementsort.Items.Add(actType.Omschrijving);
            }

            btnAddPerformance = new Button
            {
                Text = "Voeg Prestatie toe"
            };

            btnshowListPerformances = new Button
            {
                Text = "lijst presatie's"
            };

            achievementsortLayout.Children.Add(lblSort);
            achievementsortLayout.Children.Add(pckAchievementsort);

            achievementLayout.Children.Add(lblAchievement);
            achievementLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            achievementLayout.Children.Add(timeLayout);
            achievementLayout.Children.Add(achievementsortLayout);
            achievementLayout.Children.Add(btnAddPerformance);
            achievementLayout.Children.Add(btnshowListPerformances);
            achievementLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });

            finishedLayout = new StackLayout();

            lblFinished = new Label
            {
                Text = "Afgewerkt",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            };

            isFinished = new Switch
            {
                IsToggled = (bool)opdracht.Afgewerkt
            };

            finishedLayout.Children.Add(lblFinished);
            finishedLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });
            finishedLayout.Children.Add(isFinished);
            finishedLayout.Children.Add(new BoxView
            {
                Color = Color.FromHex("ddd"),
                HeightRequest = 1,
                VerticalOptions = LayoutOptions.Fill
            });

            btnSendAppointment = new Button
            {
                Text = "Verzend afspraak"
            };

            layout.Children.Add(generalInfoLayout);
            layout.Children.Add(ContactLayout);
            layout.Children.Add(articleLayout);
            layout.Children.Add(previousLayout);
            layout.Children.Add(reportLayout);
            layout.Children.Add(achievementLayout);
            layout.Children.Add(finishedLayout);
            layout.Children.Add(btnSendAppointment);

            btnAddReport.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new ReportPage(DataController.Instance.GetAchievementsFromAssessment(opdracht)[0]));
            };

            btnListSoldArticles.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new ListSoldProducts(opdracht));
            };

            btnListContacts.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new ListContacts(DataController.Instance.GetAllContactsFromACompany(DataController.Instance.GetCompanyFromAssessment(opdracht).ID)));
            };

            btnSendAppointment.Clicked += (object sender, EventArgs e) => {
                if (isFinished.IsToggled == true)
                {
                    opdracht.Afgewerkt = true;
                    opdracht.Statuslabel = "Afgewerkt";

                    DataController.Instance.Update(opdracht);
                    SyncController.Instance.SyncNeeded();
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Error", "Gelieve het product afgewerkt aan te vinken", "Ok");
                }
            };

            btnListPreviousContact.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new ListPreviousContact(opdracht, DataController.Instance.GetCompanyFromAssessment(opdracht).Naam));
            };

            btnAddPerformance.Clicked += (object sender, EventArgs e) => {
                int prestatieid = 0;

                if (pckAchievementsort.SelectedIndex != -1)
                {
                    switch (pckAchievementsort.Items[pckAchievementsort.SelectedIndex])
                    {
                        case "Herstelling":
                            prestatieid = 1;
                            break;
                        case "Verkoop":
                            prestatieid = 2;
                            break;
                        case "Onderhoud":
                            prestatieid = 0;
                            break;
                    }
                    
                    List<PrestatiesPrestatiesoorten> lijstprestatiesoorten = DataController.Instance.GetAchievementsAchievementTypeByIDs(prestatielijst[0].ID, prestatieid);
                    
                    if (lijstprestatiesoorten.Count == 0)
                    {
                        PrestatiesPrestatiesoorten pres = new PrestatiesPrestatiesoorten
                        {
                            PrestatieID = prestatielijst[0].ID,
                            PrestatieSoortID = prestatieid
                        };

                        DependencyService.Get<IToastNotificator>().Notify(
                            ToastNotificationType.Success,
                            "Prestatie ",
                            "Presatie wordt toegevoegd",
                            TimeSpan.FromSeconds(3));

                        DataController.Instance.Insert(pres);
                        SyncController.Instance.SyncNeeded();
                    }
                    else
                    {
                        DisplayAlert("Error", "Opdracht is al toegevoegd", "Ok");
                    }

                }
                else
                {
                    DisplayAlert("Error", "Gelieve een prestatiesoort te selcteren", "Ok");
                };
            };

            btnshowListPerformances.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new ListPrestaties(prestatielijst[0].ID));
            };
        }


        protected override void OnAppearing()
        {

            Content = new ScrollView
            {
                Content = layout
            };

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Content = null;
            base.OnDisappearing();
        }
    }
}
