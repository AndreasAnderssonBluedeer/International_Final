using System;
using SQLite.Net.Attributes;

namespace CatchUp.Core
{
	public class UserStorage
	{
		
		public UserStorage()
		{
		}
		//New User
		public UserStorage(string email, string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
		}
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Email{get; set;}	//Email works as PrimaryKey in the entire app. 
		public string FirstName { get; set;}
		public string LastName { get; set;}
		public string AutoMessage { get; set;}
		public bool Vibration { get; set;}
		public bool Sound { get; set; }
		public bool Mode { get; set;}



		                    
	}
}

