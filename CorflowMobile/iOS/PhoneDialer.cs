using System;
using UIKit;
using Foundation;

[assembly: Xamarin.Forms.Dependency (typeof (CorflowMobile.iOS.PhoneDialer))]

namespace CorflowMobile.iOS
{
	public class PhoneDialer : IDialer {

		public bool Dial(string number) {
			return UIApplication.SharedApplication.OpenUrl(
				new NSUrl("tel:" + number));
		}
	}
}

