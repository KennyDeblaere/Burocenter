using System;

using Xamarin.Forms;
using CorflowMobile.Models;
using System.ComponentModel;
using CorflowMobile.Data;
using Connectivity.Plugin;
using Connectivity.Plugin.Abstractions;
using Toasts.Forms.Plugin.Abstractions;
using CorflowMobile.Controllers;

namespace CorflowMobile
{
	public class SyncController
	{
		private static SyncController instance;

		private SyncParams _syncParams = null;
		private bool _syncInProgress = false;
		private bool _isUserNotified = false;

		public event EventHandler<SyncParams> OnSyncCompleted = delegate { };
		public event EventHandler<Exception> OnSyncFailed = delegate { };

		private SyncController ()
		{
			CrossConnectivity.Current.ConnectivityChanged += connectivityChanged;

			_syncParams = SyncParams.LoadSavedSyncParams();

			Device.StartTimer (TimeSpan.FromMinutes(10), () =>
				{
					TrySync ();
					return true;
				});
		}

		public static SyncController Instance
		{
			get
			{
				if (instance == null)
					instance = new SyncController ();

				return instance;
			}
		}

		public static string Tekstlabel { get; set;}
		public static TimeSpan start { get; set;}

		public static string DatabasePath { get; set; }

		public bool SyncInProgress {
			get { return _syncInProgress; }
			set { _syncInProgress = value;  } 
		}

		public SyncParams SyncParams
		{
			get { return _syncParams; }
		}

		public void TrySync()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				sync ();
			}
			else
			{
				if (!_isUserNotified)
				{
					notifyUserOffline ();
					_isUserNotified = true;
				}
			}
		}

		private void sync()
		{
			if (!_syncInProgress)
			{
				_syncInProgress = true;
				DependencyService.Get<ISyncService> ().StartBackgroundSync (_syncParams);
			}
				
		}

		public void NotifySyncFailed(Exception e)
		{
			Device.BeginInvokeOnMainThread(() =>
				{
                    if (LoginController.Instance.IsLoggedIn)
                    {
                        DependencyService.Get<IToastNotificator>().Notify(
                            ToastNotificationType.Error,
                            "Synchronisatie mislukt",
                            "Een poging tot synchroniseren is mislukt. U kunt handmatig opnieuw te proberen via het sidemenu.",
                            TimeSpan.FromSeconds(12));
                    }

					if (OnSyncFailed != null)
						OnSyncFailed (this, e);
				});
		}


		public void NotifySyncCompleted(SyncParams p)
		{
			Device.BeginInvokeOnMainThread(() =>
				{
					if (!ZagDebugSchemaVersionCheck.VerifySchema())
						((CorflowMobile.App)Xamarin.Forms.Application.Current).MainPage.DisplayAlert("Database Schema Changed", "The schema for the database has been changed since this application was generated.  This may cause failures if columns have been removed.  You may want to use Zumero Application Generator to recreate this app, based on the new schema.", "Ok");

					_syncParams.SaveSyncParam();
					_syncInProgress = false;

					if (_isUserNotified)
					{
						notifyUserOnline ();
						_isUserNotified = false;
					}

					if (OnSyncCompleted != null)
						OnSyncCompleted (this, p);
				});
		}

		private void connectivityChanged(object sender, ConnectivityChangedEventArgs e)
		{
			if (CrossConnectivity.Current.IsConnected)
				sync ();
		}

		public bool HasNeverBeenSynced ()
		{
			return DependencyService.Get<IDataService> ().HasNeverBeenSynced ();
		}

		private void notifyUserOffline ()
		{
			DependencyService.Get<IToastNotificator>().Notify(
				ToastNotificationType.Info, 
				"Verbinding verbroken",
				"U werkt momenteel offline. Er zal automatisch opnieuw gesynchroniseerd worden wanneer de verbinding hersteld wordt.",
				TimeSpan.FromSeconds(12));
		}

		private void notifyUserOnline ()
		{
			DependencyService.Get<IToastNotificator>().Notify(
				ToastNotificationType.Info, 
				"Verbinding hersteld",
				"Uw wijzigingen werden gesynchroniseerd en u werkt terug met de laatste versie van de data.",
				TimeSpan.FromSeconds(12));
		}
	}
}
