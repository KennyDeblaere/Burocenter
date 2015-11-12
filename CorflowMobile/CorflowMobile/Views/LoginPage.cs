using System;

using Xamarin.Forms;
using CorflowMobile.Controllers;
using Toasts.Forms.Plugin.Abstractions;
using CorflowMobile.Models;

namespace CorflowMobile.Views
{
	public class LoginPage : ContentPage
	{
		private RelativeLayout relativeLayout;
        private Button btnLogin;

        public LoginPage ()
		{
			BackgroundColor = Color.White;

            Image imgBackground = new Image () {
				Source = new FileImageSource () { File = "burocenter_logo.jpg" },
				IsOpaque = true,
				Opacity = 0.8
			};

            BoxView shader = new BoxView () {
				Color = Color.Black.MultiplyAlpha (.5),
			};

            Image imgFace = new Image () {
				Aspect = Aspect.AspectFill,
				Source = new FileImageSource () { File = "face.png" }
			};

            Image imgDome = new Image () {
				Aspect = Aspect.AspectFill,
				Source = new FileImageSource () { File = "dome.png" }
			};

            Label lblWelcome = new Label () {
				Text = "Welkom bij Corflow Mobile",
				FontSize = 20,
				FontFamily = Device.OnPlatform("HelveticaNeue-Bold","sans-serif-black",null),
				XAlign = TextAlignment.Center,
				TextColor = Color.Black
			};

            Label lblLogin = new Label () {
				Text = "Gelieve in te loggen met je Microsoft account",
				FontSize = 12,
				FontFamily = Device.OnPlatform("HelveticaNeue-Light","sans-serif-light",null),
				XAlign = TextAlignment.Center,
				TextColor = Color.FromHex("#666")
			};

            btnLogin = new Button {
				Text = "Log in",
				TextColor = Device.OnPlatform (Color.Black, Color.White, Color.White),
				FontFamily = Device.OnPlatform("HelveticaNeue","sans-serif",null),
                IsEnabled = !SyncController.Instance.HasNeverBeenSynced()
			};

			btnLogin.Clicked += BtnLogin_OnClicked;

            ContentView details = new ContentView () {
				Content = new StackLayout () {
					Padding = new Thickness(20,10),
					Children = {
						lblWelcome,
						lblLogin,
						btnLogin
					}
				}
			};

			relativeLayout = new RelativeLayout () {
				HeightRequest = 100,
			};

			relativeLayout.Children.Add (
                imgBackground,
				Constraint.Constant (0),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height * .5;
				})
			);

			relativeLayout.Children.Add (
				shader,
				Constraint.Constant (0),
				Constraint.Constant (0),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height * .5;
				})
			);

			relativeLayout.Children.Add (
				imgDome,
				Constraint.Constant (-10),
				Constraint.RelativeToParent ((parent) => {
					return (parent.Height * .5) - 50;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width + 10;
				}),
				Constraint.Constant (75)
			);

			relativeLayout.Children.Add (
				imgFace, 
				Constraint.RelativeToParent ((parent) => {
					return ((parent.Width / 2) - (imgFace.Width / 2));
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Height * .25;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width * .5;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width * .5;
				})
			);

			relativeLayout.Children.Add (
				details,
				Constraint.Constant (0),
				Constraint.RelativeToView (imgDome, (parent, view) => {
					return view.Y + view.Height + 10;
				}),
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}),
				Constraint.Constant (120)
			);

			imgFace.SizeChanged += (sender, e) => {
				relativeLayout.ForceLayout ();
			};
		}

		private void BtnLogin_OnClicked(object sender, EventArgs e)
		{
			LoginController.Instance.Login ();
		}

		private void onLoggedIn(Object sender, int userid)
		{
			((CorflowMobile.App)Xamarin.Forms.Application.Current).ShowRootPage ();
        }

        private void SyncCompleted(object sender, SyncParams e)
        {
            DependencyService.Get<IToastNotificator>().Notify(
                ToastNotificationType.Success,
                "Synchronisatie gelukt",
                "Er werd succesvol gesynchroniseerd.",
                TimeSpan.FromSeconds(3));

            btnLogin.IsEnabled = true;
        }

        private void SyncFailed(object sender, Exception e)
        {
            DependencyService.Get<IToastNotificator>().Notify(
                ToastNotificationType.Error,
                "Synchronisatie mislukt",
                "Lokale data kon niet worden gesynchroniseerd. Er kan niet worden ingelogd vooralleer er gesynchroniseerd is. Probeer te veranderen van internetverbinding en de applicatie af te sluiten en opnieuw op te starten.",
                TimeSpan.FromSeconds(12));
        }

        protected override void OnAppearing()
		{
			Content = relativeLayout;
			base.OnAppearing();

			LoginController.Instance.OnLoggedInSuccess += onLoggedIn;

            if (SyncController.Instance.HasNeverBeenSynced())
            {
                SyncController.Instance.OnSyncCompleted += SyncCompleted;
                SyncController.Instance.OnSyncFailed += SyncFailed;
            }
        }

		protected override void OnDisappearing()
		{
			Content = null;
			base.OnDisappearing();

			LoginController.Instance.OnLoggedInSuccess -= onLoggedIn;

            SyncController.Instance.OnSyncCompleted -= SyncCompleted;
            SyncController.Instance.OnSyncFailed -= SyncFailed;
        }
	}
}