using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using CatchUp.Core.Interfaces;
using System.Collections.ObjectModel;
using CatchUp.Core.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;

// Author: Andreas Andersson n9795383, Marie-Luise Lux n9530801, Samuel Blight n8312885

namespace CatchUp.Core.ViewModels
{
	public class ContactViewModel
		: MvxViewModel
	{
		private IDialogService dialog;
		private ILocalContactDatabase dblocal;
		private IContactRequestDatabase dbRequest;
		private IUserStorageDatabase dbOwnUser;


		private string emailContact;
		public string EmailContact
		{
			get { return emailContact; }
			set
			{
				if (value != null)
				{
					SetProperty(ref emailContact, value);
				}
			}
		}
		private string firstNameContact;
		public string FirstNameContact
		{
			get { return firstNameContact; }
			set
			{
				if (value != null)
				{
					SetProperty(ref firstNameContact, value);
				}
			}
		}
		private string lastNameContact;
		public string LastNameContact
		{
			get { return lastNameContact; }
			set
			{
				if (value != null)
				{
					SetProperty(ref lastNameContact, value);
				}
			}
		}
		private string emailRequest;
		public string EmailRequest
		{
			get { return emailRequest; }
			set
			{
				if (value != null)
				{
					SetProperty(ref emailRequest, value);
				}
			}
		}

		private string firstNameRequest;
		public string FirstNameRequest
		{
			get { return firstNameRequest; }
			set
			{
				if (value != null)
				{
					SetProperty(ref firstNameRequest, value);
				}
			}
		}
		private string lastNameRequest;
		public string LastNameRequest
		{
			get { return lastNameRequest; }
			set
			{
				if (value != null)
				{
					SetProperty(ref lastNameRequest, value);
				}
			}
		}

		//ContactList
		private ObservableCollection<LocalContact> contactItems;
		public ObservableCollection<LocalContact> ContactItems
		{
			get { return contactItems; }
			set { SetProperty(ref contactItems, value); }

		}

		//RequestList
		private ObservableCollection<ContactRequest> requestItems;
		public ObservableCollection<ContactRequest> RequestItems
		{
			get { return requestItems; }
			set { SetProperty(ref requestItems, value); }

		}


		public ICommand BtnAddContactCommand { get; private set; }
		public ICommand SelectContactCommand { get; private set; }
		public ICommand SelectRequestCommand { get; private set; }
		public ContactViewModel(IDialogService dialog, ILocalContactDatabase dblocal, IContactRequestDatabase dbRequest, IUserStorageDatabase dbOwnUser)
		{
			this.dialog = dialog;
			this.dblocal = dblocal;
			this.dbRequest = dbRequest;
			this.dbOwnUser = dbOwnUser;
			ContactItems = new ObservableCollection<LocalContact>();
			RequestItems = new ObservableCollection<ContactRequest>();
			//dblocal.CreateLocalContact("Rand3@rand.com", "RandFirs3", "RandLast3");
			//dblocal.CreateLocalContact("Test2@rand.com", "TestFirs2", "TestLast2");

			BtnAddContactCommand = new MvxCommand(() =>
			{
				ShowViewModel<AddContactViewModel>();
			});

			UpdateContacts();
			UpdateRequests();


			SelectContactCommand = new MvxCommand<LocalContact>(contact =>
			{
				//call Request page
				ShowViewModel<RequestViewModel>(new { receiver = contact.EmailContact });
			});


			SelectRequestCommand = new MvxCommand<ContactRequest>(request =>
			{
				AnswerRequest(request);
			});
		}

		private async void AnswerRequest(ContactRequest request)
		{
			string text = "Do you want to accept the contact request of the user " + request.SenderFirstName + " "
			   + request.SenderLastName + " (" + request.Sender + ")?";
			int button = await dialog.Show(text, "Accept Contact Request", "Accept", "Decline");

			if (button == 2)
			{
				//acceptrequest
				await dbRequest.AnswerRequest(request.Id, true);
				await dblocal.CreateLocalContact(request.Sender, request.SenderFirstName, request.SenderLastName);
				UpdateContacts();
				UpdateRequests();
			}
			else if (button == 1)
			{
				//denyRequest
				await dbRequest.DeleteRequest(request.Id);
				UpdateRequests();
			}
		}

		private async void UpdateContacts()
		{
			//Add contacts to list from db
			IEnumerable<LocalContact> dbContactList = dblocal.GetLocalContacts().Result;
			foreach (var contact in dbContactList)
			{
				ContactItems.Add(contact);
				RaisePropertyChanged(() => ContactItems);
			}

		}
		private async void UpdateRequests()      //fill the contacts and request lists
		{
			RequestItems.Clear();
			//Add requests to list from db
			IEnumerable<ContactRequest> dbRequestList = await dbRequest.GetRequests(dbOwnUser.GetEmail().Result);
			foreach (var r in dbRequestList)
			{
				RequestItems.Add(r);
				RaisePropertyChanged(() => RequestItems);
			}
		}
	}
}

