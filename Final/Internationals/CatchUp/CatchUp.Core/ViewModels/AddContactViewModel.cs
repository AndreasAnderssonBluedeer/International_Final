using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CatchUp.Core.Models;
using MvvmCross.Core.ViewModels;
using CatchUp.Core.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;

namespace CatchUp.Core.ViewModels
{
	public class AddContactViewModel : MvxViewModel
	{

		private readonly IDialogService dialog;
		private IUserDatabase dbUser;
		private IContactRequestDatabase dbRequest;
		private IUserStorageDatabase dbOwnUser;
		private ILocalContactDatabase dbLocal;

		//ContactList
		private ObservableCollection<User> userList;
		public ObservableCollection<User> UserList
		{
			get { return userList; }
			set { SetProperty(ref userList, value); }

		}

		private string searchTerm;

		public string SearchTerm
		{
			get { return searchTerm; }
			set
			{
				SetProperty(ref searchTerm, value);
				if (searchTerm.Length > 0)
				{
					SearchUsers(searchTerm);
				}
				else
				{
					UserList = new ObservableCollection<User>();    // an empty list so no contact list will be shown.
				}
			}
		}

		public ICommand SelectUserCommand { get; private set; }


		public AddContactViewModel(IDialogService dialog, IUserDatabase dbUser,
			IContactRequestDatabase dbRequest, IUserStorageDatabase dbOwnUser, ILocalContactDatabase dbLocal)
		{
			this.dialog = dialog;
			this.dbUser = dbUser;
			this.dbRequest = dbRequest;
			this.dbOwnUser = dbOwnUser;
			this.dbLocal = dbLocal;

			userList = new ObservableCollection<User>();

			SelectUserCommand = new MvxCommand<User>(u =>
			{
				addUser(u);
			});
		}

		public async void addUser(User u)
		{

			if (await dbLocal.CheckIfExists(u.Email))
			{
				int button = await dialog.Show("You can't add a user you already have in your contact list!", "Error", "OK");
			}
			else if (await dbRequest.CheckIfExists(u.Email, dbOwnUser.GetEmail().Result))
			{
				int button = await dialog.Show("You already have a request with this user pending!", "Error", "OK");
			}
			else if (dbOwnUser.GetEmail().Result == u.Email)
			{
				int button = await dialog.Show("You can't send a contact request to yourself!", "Error", "OK");
			}
			else
			{
				string text = "Do you want to add the user " + u.FirstName + " "
				+ u.LastName + " (" + u.Email + ") to your contact list?";
				int button = await dialog.Show(text, "Send Contact Request", "Send", "Cancel");

				if (button == 2)
				{
					await dbRequest.CreateContactRequest(dbOwnUser.GetEmail().Result, dbOwnUser.GetFirstName().Result, dbOwnUser.GetLastName().Result, u.Email);
					Close(this);
				}
			}
		}

		public async void SearchUsers(string searchTerm)
		{
			userList.Clear();
			//searchthe user list
			var dbUserList = await dbUser.GetUsers(searchTerm);
			Debug.WriteLine("The search method was called.");
			foreach (var u in dbUserList)
			{
				userList.Add(u);
				RaisePropertyChanged(() => UserList);
			}
		}
	}
}

