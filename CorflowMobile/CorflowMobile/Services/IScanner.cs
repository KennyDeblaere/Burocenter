using System;
using System.Threading.Tasks;

namespace CorflowMobile
{
	public interface IScanner
	{
		Task<ScanResult> Scan();
	}

	public class ScanResult
	{
		public string Text { get; set; }
	}
}

