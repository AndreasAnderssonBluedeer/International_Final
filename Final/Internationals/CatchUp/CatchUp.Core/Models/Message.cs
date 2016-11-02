using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace CatchUp.Core.Models
{
	public class Message
	{
		public Message()
		{
		}
		public Message(string chatId, string sender,string text,
						string locCord, double latitude, double longitude, string locText)
		{
			ChatID = chatId;
			Sender = sender;
			Text = text;
			LocationCoordinates = locCord;
            Latitude = latitude;
            Longitude = longitude;
			LocationText = locText;
		}

		public string Id { get; set; }

		public string ChatID { get; set; }
		public string Sender { get; set;}	//email
		public string Text{ get; set; }
		public string DateTime{ get; set; }
		public string LocationCoordinates{ get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string LocationText{ get; set; }
		public string CreateDat { get; set;}
	}
}

