using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using CatchUp.Core.Interfaces;
using System.Collections.Generic;
using CatchUp.Core.Models;
using System.Collections.ObjectModel;

// Author: Andreas Andersson n9795383, Marie-Luise Lux n9530801, Samuel Blight n8312885

namespace CatchUp.Core.ViewModels
{
	public class HomeViewModel
		: MvxViewModel
	{
		private IDialogService dialog;
		private IUserStorageDatabase dbOwnUser;
		private IUserStorageDatabase db;
		private IContactRequestDatabase dbRequest;
		private IUserDatabase dbUser;
		private ILocalContactDatabase dbContact;

		public ICommand BtnNotificationCommand { get; private set; }
		public ICommand BtnOptionCommand { get; private set; }
		public ICommand BtnRequestCommand { get; private set; }
		public ICommand BtnContactCommand { get; private set; }

		public void LoadStorage()
		{
			//db.CreateUser("","","");
			if (db.UserExists().Result == false)
			{
				ShowViewModel<CreateUserViewModel>();
			}
			else
			{
				CheckRequestResponses();
			}
		}


		public HomeViewModel(IDialogService dialog, IUserStorageDatabase db,
			IContactRequestDatabase dbRequest, IUserStorageDatabase dbOwnUser,
			IUserDatabase dbUser, ILocalContactDatabase dbContact)
		{
			this.dialog = dialog;
			this.dbRequest = dbRequest;
			this.db = db;
			this.dbOwnUser = dbOwnUser;
			this.dbUser = dbUser;
			this.dbContact = dbContact;

			LoadStorage();

			BtnNotificationCommand = new MvxCommand(() =>
		   {
			   ShowViewModel<NotificationViewModel>();
		   });
			BtnOptionCommand = new MvxCommand(() =>
		   {
			   ShowViewModel<OptionsViewModel>();
		   });
			BtnRequestCommand = new MvxCommand(() =>
		   {
			   ShowViewModel<RequestViewModel>();
		   });
			BtnContactCommand = new MvxCommand(() =>
		   {
			   ShowViewModel<ContactViewModel>();

		   });
		}
		private async void CheckRequestResponses()
		{
			IEnumerable<ContactRequest> dbRequestList = await dbRequest.GetAnsweredRequests(dbOwnUser.GetEmail().Result);
			foreach (var r in dbRequestList)
			{
				string text = "The user " + r.Receiver + " accepted your contact request";
				int button = await dialog.Show(text, "Contact Request Accpted", "OK");

				var u = await dbUser.GetUser(r.Receiver);
				await dbContact.CreateLocalContact(u.Email, u.FirstName, u.LastName);
				await dbRequest.DeleteRequest(r.Id);
			}
		}
	}
}

