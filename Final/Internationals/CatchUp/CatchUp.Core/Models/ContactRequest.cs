using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace CatchUp.Core.Models
{
	public class ContactRequest
	{
		public ContactRequest()
		{
		}
		public ContactRequest(string sender, string senderFirstName, string senderLastName, string receiver)
		{
			Sender = sender;
			SenderFirstName = senderFirstName;
			SenderLastName = senderLastName;
			Receiver = receiver;
		}

		public string Id { get; set; }

		public string Sender { get; set;}
		public string SenderFirstName { get; set; }
		public string SenderLastName { get; set; }
		public string Receiver { get; set;}
		public bool AnsweredRequest { get; set;}
		public bool Accepted { get; set;}


	}
}

