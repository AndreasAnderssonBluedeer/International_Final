using System;
namespace CatchUp.Core.Models
{
	public class GeoLocation
	{
		public GeoLocation() { }
		public GeoLocation(double latitude, double longitude, double? altitude = null)
		{
			Latitude = latitude;
			Longitude = longitude;
			if(altitude == null) {
				HasAltitude = false;
			}
			else {
				HasAltitude = true;
			}
			Altitude = altitude;
		}
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public double? Altitude { get; set; }
		public bool HasAltitude { get; set; }
		public string Locality { get; set; }
	}

}
