using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace CatchUp.Droid.Views
{
	[Activity(Label = "View for NotificationViewModel")]
	public class NotificationView : MvxActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			//Andreas: SetContentView(Resource.Layout.Options);
			//Marie: SetContentView(Resource.Layout.Response);
			//Samuel: SetContentView(Resource.Layout.Response);
			SetContentView(Resource.Layout.Notification);
		}
	}
}
