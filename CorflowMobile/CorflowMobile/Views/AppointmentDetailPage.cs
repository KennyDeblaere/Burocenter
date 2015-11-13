﻿using CorflowMobile.Data;
using CorflowMobile.Models;
using CorflowMobile.Superclasses;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace CorflowMobile.Views
{
    public class AppointmentDetailPage : ContentPage
    {
        private StackLayout layout, generalInfoLayout, ContactLayout, articleLayout, reportLayout, finishedLayout, achievementsortLayout, achievementLayout, timeLayout, previousLayout;
        private Label lblGeneralInfo, lblAddress, lblBTW, lblContact, lblSoldArticle, lblReport, lblFinished, lblSort, lblAchievement, lblDuration, lblMinutes, lblPrevious;
        private Button btnAddReport, btnSendAppointment, btnListSoldArticles, btnListContacts, btnListPreviousContact;
        private Switch isFinished;
        private Picker pckAchievementsort;

        private List<Prestatie> prestatielijst;

        private string formattedAddress;
        private SyncItems syncItems;

        public AppointmentDetailPage(Opdracht opdracht)
        {
            syncItems = new SyncItems();
            formattedAddress = syncItems.FormattedAddress(syncItems.GetCompanyFromAssessment(opdracht).ID);
            prestatielijst = syncItems.GetAchievements();

            Title = syncItems.GetCompanyFromAssessment(opdracht).Naam;
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
                Text = "BTW: " + syncItems.GetCompanyFromAssessment(opdracht).BTW,
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

            foreach (Prestatiesoort actType in syncItems.GetActivityType().OrderBy(t => t.ID))
            {
                pckAchievementsort.Items.Add(actType.Omschrijving);
            }

            pckAchievementsort.SelectedIndex = prestatielijst[0].PrestatiesoortID;

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
                Navigation.PushAsync(new ReportPage(syncItems.GetAchievementsFromAssessment(opdracht)[0]));
            };

            btnListSoldArticles.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new ListSoldProducts(opdracht));
            };

            btnListContacts.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new ListContacts(syncItems.GetAllContactsFromACompany(syncItems.GetCompanyFromAssessment(opdracht).ID)));
            };

            btnSendAppointment.Clicked += (object sender, EventArgs e) => {
                if (isFinished.IsToggled == true && pckAchievementsort.SelectedIndex != -1)
                {
                    opdracht.Afgewerkt = true;
                    opdracht.Statuslabel = "Afgewerkt";

                    switch (pckAchievementsort.Items[pckAchievementsort.SelectedIndex])
                    {
                        case "Herstelling":
                            prestatielijst[0].PrestatiesoortID = 1;
                            break;
                        case "Verkoop":
                            prestatielijst[0].PrestatiesoortID = 2;
                            break;
                        case "Onderhoud":
                            prestatielijst[0].PrestatiesoortID = 0;
                            break;
                    }

                    DependencyService.Get<IDataService>().Update(prestatielijst[0]);
                    DependencyService.Get<IDataService>().Update(opdracht);
                    SyncController.Instance.TrySync();
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Error", "Gelieve een prestatiesoort te selcteren. of het product afgewerkt aan te vinken", "Ok");
                }
            };

            btnListPreviousContact.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new ListPreviousContact(opdracht, syncItems.GetCompanyFromAssessment(opdracht).Naam));
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
