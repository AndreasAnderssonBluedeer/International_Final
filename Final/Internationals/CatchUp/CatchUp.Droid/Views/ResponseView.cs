using System;
using Android.App;
using Android.Gms.Maps;
using Android.OS;
using CatchUp.Core.Models;
using CatchUp.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace CatchUp.Droid.Views
{
	[Activity(Label = "View for ResponseViewModel")]
	public class ResponseView : MvxActivity, IOnMapReadyCallback
    {
        private delegate IOnMapReadyCallback OnMapReadyCallback();
        private GoogleMap map;
        ResponseViewModel vm;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Response);
            vm = ViewModel as ResponseViewModel;
            var mapFragment = FragmentManager.FindFragmentById(Resource.Id.weathermap) as MapFragment;
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.MyLocationEnabled = true;
            //vm.SetCoords(googleMap.MyLocation.Latitude, googleMap.MyLocation.Longitude);
        }

        private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            map.MyLocationChange -= Map_MyLocationChange;
            var location = new GeoLocation(e.Location.Latitude, e.Location.Longitude, e.Location.Altitude);
            //vm.SetCoords(googleMap.MyLocation.Latitude, googleMap.MyLocation.Longitude);
        }
    }
}
