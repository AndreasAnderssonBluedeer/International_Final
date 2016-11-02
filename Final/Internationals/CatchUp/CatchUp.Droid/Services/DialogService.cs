using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Threading.Tasks;
using CatchUp.Core.Interfaces;

namespace CatchUp.Droid.Services
{
	public class DialogService : IDialogService
	{
		Dialog dialog = null;

		public async Task<int> Show(string message, string title)
		{
			return await Show(message, title, "OK", "Cancel");
		}

		public async Task<int> Show(string message, string title, string confirmButton)
		{
			// 0 == click outside of box, nothing happes
			// 2 == confirm/pos. option clicked
			bool buttonPressed = false;
			int chosenOption = 0;
			Application.SynchronizationContext.Post(_ =>
			{
				var mvxTopActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
				Android.App.AlertDialog.Builder alertDialog = new AlertDialog.Builder(mvxTopActivity.Activity);
				alertDialog.SetTitle(title);
				alertDialog.SetMessage(message);
				alertDialog.SetPositiveButton(confirmButton, (s, args) =>
				{
					chosenOption = 2;
				});

				dialog = alertDialog.Create();
				dialog.DismissEvent += (object sender, EventArgs e) =>
				{
					buttonPressed = true;
					dialog.Dismiss();
				};
				dialog.Show();
			}, null);
			while (!buttonPressed)
			{
				await Task.Delay(100);
			}
			return chosenOption;
		}

		public async Task<int> Show(string message, string title, string confirmButton, string cancelButton)
		{   // 0 == click outside of box, nothing happes
			// 1 == calcel/neg. option clicked
			// 2 == confirm/pos. option clicked
			bool buttonPressed = false;
			int chosenOption = 0;
			Application.SynchronizationContext.Post(_ =>
			{
				var mvxTopActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
				Android.App.AlertDialog.Builder alertDialog = new AlertDialog.Builder(mvxTopActivity.Activity);
				alertDialog.SetTitle(title);
				alertDialog.SetMessage(message);
				alertDialog.SetNegativeButton(cancelButton, (s, args) =>
				{
					chosenOption = 1;
				});
				alertDialog.SetPositiveButton(confirmButton, (s, args) =>
				{
					chosenOption = 2;
				});

				dialog = alertDialog.Create();
				dialog.DismissEvent += (object sender, EventArgs e) =>
				{
					buttonPressed = true;
					dialog.Dismiss();
				};
				dialog.Show();
			}, null);
			while (!buttonPressed)
			{
				await Task.Delay(100);
			}
			return chosenOption;
		}
	}
}