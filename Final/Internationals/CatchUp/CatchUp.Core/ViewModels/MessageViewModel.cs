using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using CatchUp.Core.Models;
using CatchUp.Core.Interfaces;
using System.Collections.ObjectModel;
using System;

// Author: Andreas Andersson n9795383, Marie-Luise Lux n9530801, Samuel Blight n8312885

namespace CatchUp.Core.ViewModels
{
	public class MessageViewModel
		: MvxViewModel
	{

		private IMessageDatabase msgdb;
		private ILocalContactDatabase localdb;
		private string chatID,user1,user2,user,receiver;


		private ObservableCollection<Message> msgList;
		public ObservableCollection<Message> MsgList
		{
			get { return msgList; }
			set { SetProperty(ref msgList, value); }

		}


		public ICommand BtnRespondCommand {get; private set;}
		public MessageViewModel(IMessageDatabase msgdb, ILocalContactDatabase localdb)
		{
			
			this.msgdb = msgdb;
			this.localdb = localdb;
			msgList = new ObservableCollection<Message>();

			BtnRespondCommand = new MvxCommand(() =>
			{
				
				string text=null;
				foreach (var m in msgList)
				{
					text = m.Text;
				}



				ShowViewModel<ResponseViewModel>(new { receiver = receiver, user = user,
					text = text, chatID = chatID});
			});
		  

		}



		public void Init(string chatID,string user1,string user2, string user)
		{
			this.chatID = chatID;
			this.user1 = user1;
			this.user2 = user2;
			this.user = user;
			if (user1 == user)
			{
				receiver = user2;
			}
			else {
				receiver = user1;
			}
			ShowMessages();
		}

		public async void ShowMessages()
		{
			var templist = await msgdb.GetMessages(chatID);
			string you = "You:";
			string chatter = localdb.GetLocalContact(receiver).Result.FirstNameContact+":";

			foreach (var u in templist)
			{
				Message m = u;
				if (m.Sender==user)
				{
					m.Sender = you;
				}
				else {
					m.Sender = chatter;
				}
				msgList.Add(m);
				RaisePropertyChanged(() => MsgList);
			}
		}

	}
}

