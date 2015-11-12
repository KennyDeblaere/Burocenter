using CorflowMobile.Controllers;
using CorflowMobile.Views;
using CorflowMobile.Models;
using System;

using Xamarin.Forms;
using CorflowMobile.Data;

[assembly: Xamarin.Forms.Dependency(typeof(CorflowMobile.Data.DataService))]
namespace CorflowMobile
{
	public class App : Application
	{
        public App ()
		{
			if (SyncController.Instance.HasNeverBeenSynced ())
				MainPage = new SplashPage ();
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

