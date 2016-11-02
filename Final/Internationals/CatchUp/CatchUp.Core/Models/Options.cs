using System;
namespace CatchUp.Core.Models
{
	public class Options
	{
		public Options()
		{
		}

		public bool Sound { get; set; }
		public bool Vibration { get; set;}
		public bool DisturbMode { get; set;}
		public string AutoMessage { get; set; }
		public string FirstName { get; set;}
		public string LastName { get; set;}

	}
}
