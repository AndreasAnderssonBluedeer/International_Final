using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using CatchUp.Core.Interfaces;
using MvvmCross.Plugins.Validation;
using System.Diagnostics;
using CatchUp.Core.Models;

// Author: Andreas Andersson n9795383, Marie-Luise Lux n9530801, Samuel Blight n8312885

namespace CatchUp.Core.ViewModels
{
	public class ResponseViewModel
		: MvxViewModel
	{
		private ILocalContactDatabase dblocal;
		private IMessageDatabase dbMsg;
		private IChatDatabase dbChat;
		private IUserStorageDatabase dbUStore;
		private string user,receiver,text,chatID,res;
        private IGeoCoder geocoder;
        private GeoLocation myLocation;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        //Code Marie

        private string responseText = "";
		public string ResponseText
		{
			get { return responseText; }
			set
			{
				if (value != null && value != responseText)
				{
					responseText = value;
					RaisePropertyChanged(() => ResponseText);
				}
			}
		}

		private string reqText = "Request Text";
		public string ReqText
		{
			get { return reqText; }
			set
			{
				if (value != null && value != reqText)
				{
					reqText = value;
					RaisePropertyChanged(() => ReqText);
				}
			}
		}

		private string locText = "Request Text";
		public string LocText
		{
			get { return locText; }
			set
			{
				if (value != null && value != locText)
				{
					locText = value;
					RaisePropertyChanged(() => LocText);
				}
			}
		}

		private string title = "Respond to ";
		public string Title
		{
			get { return title; }
			set
			{
				if (value != null && value != title)
				{
					
					title = res;
					RaisePropertyChanged(() => Title);
				}
			}
		}
		//End of Marie's Code.
		//Marie Code
		public IMvxCommand SendResponse { get; private set; }
		public ICommand BtnLocCommand { get; private set;}
		//End of Marie's Code

		IMvxToastService toastService;
		IValidator validator;

		private bool location = false;
		private string gps="coord";

		public ResponseViewModel(ILocalContactDatabase dbLocal, IMessageDatabase dbMsg, IChatDatabase dbChat
		                        , IValidator validator, IMvxToastService toastService, IGeoCoder geocoder)
		{
			this.dblocal = dbLocal;
			this.dbMsg = dbMsg;
			this.dbChat = dbChat;

			this.toastService = toastService;
			this.validator = validator;
            //Marie Code

			BtnLocCommand = new MvxCommand(() =>
			{
				
				if (location)
				{
					location = false;
				}
				else {
					location = true;
				}
			});

			//End of Marie's Code
		}
		public override void Start()
		{
			SendResponse = new MvxCommand(SendMsg);
		}
		public void Init(string receiver, string user, string text,string chatID)
		{
			this.receiver = receiver;
			this.user = user;
			ReqText = text;
			this.chatID = chatID;
			SetTitle();


		}

        public void SetCoords(double lati, double longi)
        {
            MyLocation = new GeoLocation(lati, longi);
        
        }

		public async void SendMsg()
		{
			if (location)
			{
                //Get GPS COORD#


                gps = "coord12";
			}
			//chat exists.
			var exist = await dbChat.CheckIfExists(user, receiver);
			if (!exist)
			{
				await dbChat.CreateChat(user, receiver);
				await dbMsg.CreateMessage(await dbChat.GetChatID(
					user,receiver), user, responseText, gps, 0.0, 0.0, locText);
			}
			else
			{
				await dbMsg.CreateMessage(await dbChat.GetChatID(
					user, receiver), user, responseText, gps, 0.0, 0.0, locText);
			}

			toastService.DisplayMessage("Response Sent.");
            ShowViewModel<NotificationViewModel>();

        }

		public void SetTitle()
		{
			LocalContact contact= dblocal.GetLocalContact(receiver).Result;

			Debug.WriteLine("Contact:"+contact.FirstNameContact);
			res = "Respond to " +  contact.FirstNameContact + " " +
													  contact.LastNameContact;
			Title = res;
		}


	}
}

