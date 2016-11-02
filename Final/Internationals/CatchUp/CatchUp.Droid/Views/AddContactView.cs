using System;
using MvvmCross.Droid.Views;
using Android.App;
using Android.OS;
namespace CatchUp.Droid.Views
{
		[Activity(Label = "View for AddContactViewModel")]
		public class AddContactView : MvxActivity
		{
			protected override void OnCreate(Bundle bundle)
			{
				base.OnCreate(bundle);
				//Andreas: SetContentView(Resource.Layout.Options);
				//Marie: SetContentView(Resource.Layout.Response);
				//Samuel: SetContentView(Resource.Layout.Response);
				SetContentView(Resource.Layout.AddContact);
			}
		}
	}
