using System;
using Xamarin.Forms;
using CorflowMobile.Models;
using CorflowMobile.Data;
using Connectivity.Plugin;
using Connectivity.Plugin.Abstractions;
using Toasts.Forms.Plugin.Abstractions;

namespace CorflowMobile.Controllers
{
	public class SyncController
	{
		private static SyncController instance;
        private static string _dbPath;

        private SyncParams _syncParams = null;
		private bool _syncInProgress = false;
		private bool _userNotified = false;
        private bool _syncNeeded = false;

		public event EventHandler<SyncParams> OnSyncCompleted = delegate { };
		public event EventHandler<Exception> OnSyncFailed = delegate { };

		private SyncController ()
		{
			CrossConnectivity.Current.ConnectivityChanged += (object sender, ConnectivityChangedEventArgs e) => { TrySync (); };

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

		public static string DatabasePath
        {
            get { return _dbPath; }
            set { _dbPath = value; }
        }

        public bool SyncInProgress
        {
			get { return _syncInProgress; }
		}

        public bool HasNeverBeenSynced()
        {
            return DependencyService.Get<IDataService>().HasNeverBeenSynced();
        }

        public void SyncNeeded()
        {
            _syncNeeded = true;
        }

        public void TrySync()
		{
            if (_syncNeeded)
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (!_syncInProgress)
                    {
                        _syncInProgress = true;
                        DependencyService.Get<ISyncService>().StartBackgroundSync(_syncParams);
                    }
                }
                else
                {
                    if (!_userNotified)
                    {
                        notifyUserOffline();
                        _userNotified = true;
                    }
                }
            }
        }

        public void NotifySyncCompleted(SyncParams p)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!ZagDebugSchemaVersionCheck.VerifySchema())
                    ((CorflowMobile.App)Xamarin.Forms.Application.Current).MainPage.DisplayAlert("Database Schema Changed", "The schema for the database has been changed since this application was generated.  This may cause failures if columns have been removed.  You may want to use Zumero Application Generator to recreate this app, based on the new schema.", "Ok");

                _syncParams.SaveSyncParam();
                _syncInProgress = false;
                _syncNeeded = false;

                if (_userNotified)
                {
                    notifyUserOnline();
                    _userNotified = false;
                }

                if (OnSyncCompleted != null)
                    OnSyncCompleted(this, p);
            });
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
