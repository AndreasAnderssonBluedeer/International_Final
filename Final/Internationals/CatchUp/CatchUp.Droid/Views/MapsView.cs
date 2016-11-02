using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using CatchUp.Core.Models;
using CatchUp.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace CatchUp.Droid.Views
{
	[Activity(Label = "View for HomeViewModel")]
	public class MapsView : MvxActivity, IOnMapReadyCallback
	{
		private delegate IOnMapReadyCallback OnMapReadyCallback();
		private GoogleMap map;
		MapsViewModel vm;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Map);
			vm = ViewModel as MapsViewModel;
			var mapFragment = FragmentManager.FindFragmentById(Resource.Id.weathermap) as MapFragment;
			mapFragment.GetMapAsync(this);
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			vm.OnMapSetup(MoveToLocation, AddWeatherPin);
			map = googleMap;
			map.MyLocationEnabled = true;
			map.MyLocationChange += Map_MyLocationChange;
			map.MapLongClick += Map_MapClick;
		}

		private void Map_MapClick(object sender, GoogleMap.MapLongClickEventArgs e)
		{
			vm.MapTapped(new GeoLocation(e.Point.Latitude, e.Point.Longitude));
		}

		private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
		{
			map.MyLocationChange -= Map_MyLocationChange;
			var location = new GeoLocation(e.Location.Latitude, e.Location.Longitude, e.Location.Altitude);
			MoveToLocation(location);
			vm.OnMyLocationChanged(location);
		}

		private void MoveToLocation(GeoLocation geoLocation, float zoom = 18)
		{
			var markerOptions = new MarkerOptions();
			markerOptions.SetPosition(new LatLng(vm.MyLocation.Latitude, vm.MyLocation.Longitude));
			markerOptions.SetSnippet("Lat:" + geoLocation.Latitude + " Long:" + geoLocation.Longitude);
			markerOptions.SetTitle(geoLocation.Locality);
			map.AddMarker(markerOptions);

			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(new LatLng(vm.MyLocation.Latitude, vm.MyLocation.Longitude));
			builder.Zoom(zoom);
			var cameraPosition = builder.Build();
			var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

			map.MoveCamera(cameraUpdate);
		}
		private void AddWeatherPin(GeoLocation location)
		{
			var markerOptions = new MarkerOptions();
			markerOptions.SetPosition(new LatLng(location.Latitude, location.Longitude));
		//	var min = forecast.DailyForecasts.FirstOrDefault().Temperature.Minimum;
		//	var max = forecast.DailyForecasts.FirstOrDefault().Temperature.Maximum;
			markerOptions.SetSnippet("Lat:"+location.Latitude+ " Long:" +location.Longitude);
			markerOptions.SetTitle(location.Locality);
			map.AddMarker(markerOptions);
		}
	}
}