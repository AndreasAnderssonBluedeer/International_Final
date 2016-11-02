using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchUp.Core.Models
{
	public class User
	{
		public User()
		{
		}
		public User(string email, string firstName, string lastName)
		{
			Email = email;
			FirstName = firstName;
			LastName = lastName;
		}

		public string Id { get; set; }  //DOESN'T WORK WITHOUT IT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



		public string Email { get; set; }
		public string FirstName { get; set;}
		public string LastName { get; set; }


	}
}

