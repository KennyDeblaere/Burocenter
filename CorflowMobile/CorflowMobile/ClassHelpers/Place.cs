using System;

namespace CorflowMobile
{
	public class Place
	{
		public string Name { get; set; }
		public string Vicinity { get; set; }
		public Geocode Location { get; set; }
		public Uri Icon { get; set; }
	}
}

