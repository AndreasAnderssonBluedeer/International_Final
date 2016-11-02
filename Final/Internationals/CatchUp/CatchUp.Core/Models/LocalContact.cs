using System;
using SQLite.Net.Attributes;

namespace CatchUp.Core.Models
{
	public class LocalContact
	{
		public LocalContact() { }

			public LocalContact(string email, string firstName, string lastName)
			{
				EmailContact = email;
				FirstNameContact = firstName;
				LastNameContact = lastName;
			}

			[PrimaryKey, AutoIncrement]
			public int Id { get; set; }
			public string FirstNameContact { get; set; }
			public string LastNameContact { get; set; }
			public string EmailContact { get; set; }

		}



}

