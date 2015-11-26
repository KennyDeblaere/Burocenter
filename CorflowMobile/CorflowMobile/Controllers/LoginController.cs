using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using CorflowMobile.Models;
using CorflowMobile.Data;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace CorflowMobile.Controllers
{
	public class LoginController
	{
		private static LoginController instance;

		private const string clientId = "99614058-6a30-41c8-a23d-e3ef29bb6b9e";
        private const string authority = "https://login.windows.net/common";
		private const string returnUri = "http://authentication-redirect";
		private const string graphResourceUri = "https://graph.windows.net";
		private const string graphApiVersion = "2013-11-08";
		private bool isLoggedIn = false;
        private Persoon currentUser;

		public event EventHandler<int> OnLoggedInSuccess = delegate { };
		public event EventHandler<Exception> OnLoggedInFailed = delegate { };
		public event EventHandler<Object> OnLoggedOut = delegate { };

		public static LoginController Instance
		{
			get
			{
				if (instance == null)
					instance = new LoginController ();

				return instance;
			}
		}

        public bool IsLoggedIn
		{
			get
			{
				return isLoggedIn;
			}
		}

		public Persoon GetCurrentUser
		{
			get
			{
				return currentUser;
			}
		}

		public async void Login ()
		{
			try {
				var data = await DependencyService.Get<Dependency.IAuthenticator> ().Authenticate (authority, graphResourceUri, clientId, returnUri);
				currentUser = findUserByID(data.UserInfo.GivenName, data.UserInfo.FamilyName);

                if (OnLoggedInSuccess != null)
                    OnLoggedInSuccess(this, 0);
            }
			catch (Exception e) {
				if (OnLoggedInFailed != null)
					OnLoggedInFailed (this, e);
			}
        }

		private Persoon findUserByID(string firstname, string name)
		{
            Persoon user = null;
			List<Persoon> werknemers = DataController.Instance.GetPersons().Where(t => t.Voornaam.ToLower() == firstname.ToLower() && t.Naam.ToLower() == name.ToLower() && t.Functie != 0).ToList();
            if (werknemers.Count > 0)
            {
                user = werknemers[0];
            }
            return user;
        }

		public void Logout()
		{
			DependencyService.Get<Dependency.IAuthenticator> ().Logout ();

		}
	}
}

