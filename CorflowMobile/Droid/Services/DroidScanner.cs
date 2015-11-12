using System;
using System.Threading.Tasks;
using ZXing.Mobile;
using Xamarin.Forms;

namespace CorflowMobile.Droid
{
	public class DroidScanner : IScanner
	{
		public async Task<ScanResult> Scan()
		{
			var context = Forms.Context;
			var scanner = new MobileBarcodeScanner()
			{
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
	}
}

