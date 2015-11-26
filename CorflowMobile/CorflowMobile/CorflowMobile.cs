using CorflowMobile.Controllers;
using CorflowMobile.Views;

using Xamarin.Forms;

[assembly: Dependency(typeof(CorflowMobile.Data.DataService))]
namespace CorflowMobile
{
	public class App : Application
	{
        public App ()
		{
			if (SyncController.Instance.HasNeverBeenSynced ())
            {
                SyncController.Instance.SyncNeeded();
                MainPage = new SplashPage();
            }
			else
				ShowLoginPage ();
		}


		protected override void OnStart ()
		{
			SyncController.Instance.TrySync ();
		}

		public void ShowLoginPage ()
		{
			MainPage = new LoginPage ();
		}

		public void ShowRootPage ()
		{
			MainPage = new RootPage ();
		}
	}
}

