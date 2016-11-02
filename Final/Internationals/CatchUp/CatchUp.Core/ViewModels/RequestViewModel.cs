using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using CatchUp.Core.Models;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CatchUp.Core.Interfaces;
using MvvmCross.Plugins.Validation;

// Author: Andreas Andersson n9795383, Marie-Luise Lux n9530801, Samuel Blight n8312885

namespace CatchUp.Core.ViewModels
{
	public class RequestViewModel
		: MvxViewModel
	{
		private ILocalContactDatabase dblocal;
		private IMessageDatabase dbMsg;
		private IChatDatabase dbChat;
		private IUserStorageDatabase dbUStore;
        private IDialogService dialog;
        private string user;

		private string messageText = "";
		public string MessageText
		{
			get { return messageText; }
			set
			{
				if (value != null && value != messageText)
				{
					messageText = value;
					RaisePropertyChanged(() => MessageText);
				}
			}
		}


		private string sendReqText = "Send";
		private int count = 0;
		public string SendReqText
		{
			get { return sendReqText; }
			set
			{
				count++;
				sendReqText = value + count;
				RaisePropertyChanged(() => SendReqText);
			}
		}
		//End of Samuel's Code.

		//Samuel's Code
		public IMvxCommand BtnSendReqCommand { get; private set; }
		//End of Samuel's Code
		public ICommand SelectContactCommand { get; private set; }
		public ICommand BtnLocationCommand { get; private set; }
		private bool location = false;

		public override void Start()
		{
			BtnSendReqCommand = new MvxCommand(CheckReciever);
		}

		IMvxToastService toastService;
		IValidator validator;

		public RequestViewModel(ILocalContactDatabase dblocal,IUserStorageDatabase dbUStore, 
		                        IMessageDatabase dbMsg, IChatDatabase dbChat, 
		                        IValidator validator, IMvxToastService toastService,
                                IDialogService dialog)
		{
			this.dblocal = dblocal;
			this.dbMsg = dbMsg;
			this.dbChat = dbChat;
			this.dbUStore = dbUStore;
			this.toastService = toastService;
			this.validator = validator;
            this.dialog = dialog;

			ContactItems = new ObservableCollection<LocalContact>();

			SelectContactCommand = new MvxCommand<LocalContact>(contact =>
			{
				//Do something with your listitem
				//EmailContact = contact.EmailContact;
				//FirstNameContact = contact.FirstNameContact;
				//LastNameContact = contact.LastNameContact;
				searchTerm = contact.EmailContact;
				RaisePropertyChanged(() => SearchTerm);
				ContactItems = new ObservableCollection<LocalContact>();    // an empty list so no contact list will be shown.

				//	LocalContact tempContact = contact;
				Debug.WriteLine("SelectContactCommand" + searchTerm);
			});

			/*BtnSendReqCommand = new MvxCommand(() =>
			{
				SendMsg();
			});*/

			BtnLocationCommand = new MvxCommand(() =>
			{
				if (location){location = false;}

				else { location = true; }
			});

		}

		public void Init(string receiver, string user)
		{
			if (receiver != null)
			{
				searchTerm = receiver;

			}
			if (user != null)
			{
				this.user = user;
			}
			else {
				this.user = dbUStore.GetEmail().Result;
			}
		}

        public void CheckReciever()
        {
            if (!dblocal.CheckIfExists(searchTerm).Result)
            {
                dialog.Show("You can only send messges to users in your contact list!", "Error", "OK");
            }
            else
            {
                SendMsg();
            }
        }

		public async void SendMsg()
		{
			if (location)
			{
				MessageText+= "\n\n" + dbUStore.GetFirstName().Result + " has requested your location.";
			}

			//searchTerm
		//	if (await dblocal.GetLocalContact(searchTerm) != null)
		//	{
				//chat exists.
				var exist = await dbChat.CheckIfExists(user, searchTerm);
				if (!exist)
				{
					await dbChat.CreateChat(user, searchTerm);
					await dbMsg.CreateMessage(await dbChat.GetChatID(
						user, searchTerm),user, MessageText, "Coord", 0.0, 0.0, "Library 4th floor");
				}
				else
				{
					await dbMsg.CreateMessage(await dbChat.GetChatID(
						user, searchTerm),user, MessageText, "Coord1", 0.0, 0.0, "Library 4th floor");
				}
			//	}

			//	else {
			//	}
			toastService.DisplayMessage("Request Sent.");
            ShowViewModel<HomeViewModel>();

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
					SearchContacts(searchTerm);
				}
				else
				{
					ContactItems = new ObservableCollection<LocalContact>();    // an empty list so no contact list will be shown.
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

		public async void SearchContacts(string searchtTerm)
		{
			ContactItems.Clear();
			//search and add contacts to listView from db
			IEnumerable<LocalContact> dbContactList = dblocal.SearchContacts(searchTerm).Result;
			foreach (var contact in dbContactList)
			{
				ContactItems.Add(contact);
				RaisePropertyChanged(() => ContactItems);
				Debug.WriteLine("Contact:" + contact.EmailContact + ", " + contact.FirstNameContact + ", " + contact.LastNameContact);
			}
		}
	}
}

