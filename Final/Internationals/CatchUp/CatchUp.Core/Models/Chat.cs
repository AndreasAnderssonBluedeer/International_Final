using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace CatchUp.Core.Models
{
	public class Chat
	{
		public Chat()
		{
		}
		public Chat(string user1,string user2) {

			User1 = user1;
			User2 = user2;
		}

		public string Id { get; set; }

		public string User1 { get; set; }
		public string User2 { get; set; }
		public bool User1Read { get; set; }
		public bool User2Read { get; set; }

	}
}

