using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatchUp.Core.Models;

namespace CatchUp.Core.Interfaces
{
	public interface IMessageDatabase
	{

		Task<IEnumerable<Message>> GetMessages(string chatID);   //unread messages
		//Get all messages for one conversation, sorted by date desc. 
		Task<int> CreateMessage(string id,string sender, string text,
						string locCord, double latitude, double longitude, string locText);
		




	}
}

