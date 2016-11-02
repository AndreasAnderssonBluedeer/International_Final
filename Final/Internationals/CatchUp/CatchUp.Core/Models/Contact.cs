using System;
namespace CatchUp.Core.Models
{
	public class Contact
	{
		public Contact()
		{
		}
		public Contact(string firstName, string lastName, string email)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		//Email== PrimaryKey
		public string Email { get; set; }

	}
}

