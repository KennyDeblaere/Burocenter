using System;

using Xamarin.Forms;
using CorflowMobile.Models;
using Toasts.Forms.Plugin.Abstractions;
using CorflowMobile.Controllers;

namespace CorflowMobile.Views
{
	public class SplashPage : ContentPage
	{
		private StackLayout layout;
        private Label splashLabel;

        public SplashPage ()
		{
            Image imgLogo = new Image {
				Source = "corflowmobile_logo.png"
			};

			Label logoLabel = new Label {
				Text = "Corflow Mobile",
				TextColor = Color.Black,
				FontSize = 28,
				FontFamily = Device.OnPlatform("HelveticaNeue-Bold","sans-serif-black",null)
			};

			ContentView logoLabelView = new ContentView {
				Padding = new Thickness (6, 36, 0, 0),
				Content = logoLabel
			};

			StackLayout logoLayout = new StackLayout {
				BackgroundColor = Color.White,
				Orientation = StackOrientation.Horizontal,
				Children = {
                    imgLogo,
					logoLabelView
				}
			};

			ContentView logoView = new ContentView {
				Padding = new Thickness (0, 70, 0, 0),
				HorizontalOptions = LayoutOptions.Center,
				Content = logoLayout
			};

			ActivityIndicator activityIndicator = new ActivityIndicator {
				IsRunning = true
			};

			splashLabel = new Label {
				Text = "Bezig met het synchroniseren van de lokale data ...",
				TextColor = Color.FromHex("#666"),
				FontSize = 12,
				FontFamily = Device.OnPlatform("HelveticaNeue-Light","sans-serif-light",null),
			};

			StackLayout splashLayout = new StackLayout {
				BackgroundColor = Color.White,
				Children = {
					activityIndicator,
					splashLabel
				}
			};

			ContentView splashView = new ContentView {
				Padding = new Thickness (0, 120, 0, 0),
				HorizontalOptions = LayoutOptions.Center,
				Content = splashLayout
			};
					
			layout = new StackLayout {
				BackgroundColor = Color.White,
				Children = {
					logoView,
					splashView
				}
			};
        }

        private void syncCompleted(object sender, SyncParams e)
        {
            ((CorflowMobile.App)Xamarin.Forms.Application.Current).ShowLoginPage();
        }

        private void syncFailed(object sender, Exception e)
		{
            ((CorflowMobile.App)Xamarin.Forms.Application.Current).ShowLoginPage();
        }

		protected override void OnAppearing()
		{
			Content = layout;
			base.OnAppearing();

			SyncController.Instance.OnSyncCompleted += syncCompleted;
			SyncController.Instance.OnSyncFailed += syncFailed;

			if (!SyncController.Instance.SyncInProgress) {
				((CorflowMobile.App)Xamarin.Forms.Application.Current).ShowLoginPage ();
			}
				
		}

		protected override void OnDisappearing()
		{
			Content = null;
			base.OnDisappearing();

			SyncController.Instance.OnSyncCompleted -= syncCompleted;
			SyncController.Instance.OnSyncFailed -= syncFailed;
		}
	}
}


