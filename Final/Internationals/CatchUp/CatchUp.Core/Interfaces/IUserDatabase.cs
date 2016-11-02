using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatchUp.Core.Models;

namespace CatchUp.Core
{
	public interface IUserDatabase
	{

		Task<IEnumerable<User>> GetUsers(string searchTerm);
		Task<int> CreateUser(string email, string firstName, string lastName);
		Task<bool> CheckIfExists(string email);
		Task<User> GetUser(string email);
        Task<bool> UpdateFirstName(string email, string firstname);
        Task<bool> UpdateLastName(string email, string lastname);


    }
}

