using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using CorflowMobile.Dependency;

[assembly:Dependency(typeof(CorflowMobile.Droid.Authenticator))]
namespace CorflowMobile.Droid
{
	class Authenticator : IAuthenticator
    {
		private AuthenticationContext authContext;

        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters((Activity)Forms.Context);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }


		public void Logout ()
		{
			authContext.TokenCache.Clear ();
		}
    }
}