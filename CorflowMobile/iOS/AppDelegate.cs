using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using UIKit;
using Foundation;
using XLabs.Forms.Controls;
using XLabs.Forms;
using CorflowMobile.Controllers;

[assembly: Dependency(typeof(CorflowMobile.iOS.SHA1Service))]
[assembly: Dependency(typeof(CorflowMobile.Data.BaseSyncService))]

namespace CorflowMobile.iOS
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

	[Register ("AppDelegate")]
	public partial class AppDelegate : XFormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			var sqliteFilename = "data.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);

			SyncController.DatabasePath = path;

			LoadApplication(new CorflowMobile.App());

			return base.FinishedLaunching (app, options);
		}

		public override void DidEnterBackground(UIApplication application)
		{
			System.Console.WriteLine("AppDelegate:DidEnterBackground");

			if (!DependencyService.Get<Data.ISyncService>().IsSyncRunning())
				return;

			int bgTaskId = (int)UIApplication.BackgroundTaskInvalid;

			bgTaskId = (int)UIApplication.SharedApplication.BeginBackgroundTask (() => {
				UIApplication.SharedApplication.EndBackgroundTask (bgTaskId);
			});
		}
	}
}

