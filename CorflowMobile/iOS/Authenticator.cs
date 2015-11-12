using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using CorflowMobile.Dependency;
using UIKit;
using System.Linq;
using Xamarin.Forms;

[assembly: Dependency(typeof(CorflowMobile.iOS.Authenticator))]
namespace CorflowMobile.iOS
{
    class Authenticator : IAuthenticator
    {
		private AuthenticationContext authContext;

        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(controller);
            var authResult = await authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
            return authResult;
        }


        public void Logout()
        {
            authContext.TokenCache.Clear();
        }
    }
}
