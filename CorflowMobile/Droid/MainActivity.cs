using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Toasts.Forms.Plugin.Droid;

[assembly: Dependency(typeof(CorflowMobile.Droid.SHA1Service))]
namespace CorflowMobile.Droid
{
	public class SHA1Service : CorflowMobile.Data.ISHA1Service
	{
		public string HashString(string input)
		{
			byte[] jsonBytes = Encoding.UTF8.GetBytes(input);
			var sha1 = SHA1.Create();
			byte[] hashBytes = sha1.ComputeHash(jsonBytes);

			var sb = new StringBuilder();
			foreach (byte b in hashBytes)
			{
				var hex = b.ToString("X2");
				sb.Append(hex);
			}
			return sb.ToString();
		}
	}

	[Activity (Label = "Corflow Mobile", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/CustomTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			ToastNotificatorImplementation.Init ();

			var sqliteFilename = "data.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, sqliteFilename);
			SyncController.DatabasePath = path;

			LoadApplication (new App ());
		}

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}

