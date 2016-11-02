using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using CatchUp.Core.Models;
using CatchUp.Core.Interfaces;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;

// Author: Andreas Andersson n9795383, Marie-Luise Lux n9530801, Samuel Blight n8312885

namespace CatchUp.Core.ViewModels
{
	public class MapsViewModel
		: MvxViewModel
	{

		private GeoLocation myLocation;
		private IGeoCoder geocoder;
		private Action<GeoLocation, float> moveToLocation;
		private Action<GeoLocation> weatherPinFound;
		public GeoLocation MyLocation
		{
			get { return myLocation; }
			set { myLocation = value; }
		}

		public MapsViewModel(IGeoCoder geocoder)
		{
			this.geocoder = geocoder;
            GeoLocation g = new Models.GeoLocation(40, -70);
            this.MyLocation = g;
		}
		public void OnMyLocationChanged(GeoLocation location)
		{
		//	MyLocation = location;
			GetWeatherInfo(location);
			Debug.WriteLine(location.Longitude+","+location.Latitude+","+location.Altitude);
		}
		public void MapTapped(GeoLocation location)
		{
			//GetWeatherInfo(location);
		}

		private void GetWeatherInfo(GeoLocation location)
		{
			
			//weatherPinFound(location);
		}

		public void OnMapSetup(Action<GeoLocation, float> MoveToLocation, Action<GeoLocation> WeatherPinFound)
		{
		//	moveToLocation = MoveToLocation;
		//	weatherPinFound = WeatherPinFound;
		}
	}
}

