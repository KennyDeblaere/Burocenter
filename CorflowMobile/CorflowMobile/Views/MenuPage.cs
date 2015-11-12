using System;

using Xamarin.Forms;
using CorflowMobile.Controllers;
using ImageCircle.Forms.Plugin.Abstractions;
using Toasts.Forms.Plugin.Abstractions;

namespace CorflowMobile.Views
{
	public class MenuPage : ContentPage
	{
		public ListView Menu { get; set; }

		public MenuPage ()
		{
			Icon = "settings.png";
			Title = "menu"; // The Title property must be set.
			BackgroundColor = Color.FromHex ("333333");

            Button syncButton = new Button () {
				VerticalOptions = LayoutOptions.EndAndExpand,
				HorizontalOptions = LayoutOptions.Center,
				Text = "Synchroniseer",
			};

			syncButton.Clicked += syncButton_OnClicked;

            CircleImage imgUser = new CircleImage
            {
                BorderColor = Color.FromHex("#BDC3C7"),
                BorderThickness = 3,
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Source = "face.png",
            };

            Label lblUserName = new Label()
            {
                Text = LoginController.Instance.GetCurrentUser.Voornaam + " " + LoginController.Instance.GetCurrentUser.Naam,
                TextColor = Color.White
            };

            Label lblUserFunctie = new Label()
            {
                Text = LoginController.Instance.GetCurrentUser.Functie == 2 ? "Verkoper" : "Technieker",
                FontSize = 10
            };

            Label lblLogout = new Label()
            {
                Text = "Uitloggen",
                TextColor = Color.White,
                FontSize = 12
            };

            lblLogout.GestureRecognizers.Add(new TapGestureRecognizer((sender) =>
            {
                LoginController.Instance.Logout();
                ((CorflowMobile.App)Xamarin.Forms.Application.Current).ShowLoginPage();
            }));

            ContentView currentUserView = new ContentView () {
				Content = new StackLayout () {
					Padding = new Thickness(0,10,0,25),
					Spacing = 15,
					Orientation = StackOrientation.Horizontal,
					Children = {
                        imgUser,
						new StackLayout () {
							Children = {
                                lblUserName,
								new ContentView () {
									Padding = new Thickness(0,-10,0,2),
									Content = lblUserFunctie
                                },
                                lblLogout
                            }
						}
					}
				}
			};

			Menu = new MenuListView ();

            Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (20),
				Children = {
                    currentUserView,
					new BoxView() {
						HeightRequest = 1,
						BackgroundColor = Color.FromHex ("#747474"),
					},
					Menu,
                    syncButton
				}
			};
		}

		private void syncButton_OnClicked(object sender, EventArgs e)
		{
			SyncController.Instance.TrySync ();
			DependencyService.Get<IToastNotificator>().Notify(
				ToastNotificationType.Info, 
				"Synchroniseren",
				"Er wordt gesynchroniseerd...",
				TimeSpan.FromSeconds(3));
		}
	}
}


