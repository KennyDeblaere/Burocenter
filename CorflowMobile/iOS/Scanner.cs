using System;
using System.Threading.Tasks;
using ZXing.Mobile;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency (typeof (CorflowMobile.iOS.Scanner))]

namespace CorflowMobile.iOS
{
	public class Scanner: IScanner
	{
		#region IScanner implementation

		 async Task<ScanResult> IScanner.Scan ()
		{
			var scanner = new ZXing.Mobile.MobileBarcodeScanner(){
				UseCustomOverlay = false,
				BottomText = "Scanning will happen automatically",
				TopText = "Hold your camera about \n6 inches away from the barcode",
			};
			try
			{
				var result = await scanner.Scan();
				return new ScanResult
				{
					Text = result.Text,
				};
			}
			catch (System.Exception)
			{
				return new ScanResult
				{
					Text = string.Empty,
				};
			}

		}

		#endregion


	}
}

