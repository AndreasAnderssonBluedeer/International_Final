using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatchUp.Core.Models;

namespace CatchUp.Core.Interfaces
{
	public interface ILocalContactDatabase
	{
		//Creates a local contact, used for local contactlist. returns int as id. 
		Task<int> CreateLocalContact(string email, string firstname, string lastname);
		Task<LocalContact> GetLocalContact(string email);
		Task<IEnumerable<LocalContact>> GetLocalContacts();
		Task<IEnumerable<LocalContact>> SearchContacts(string searchTerm);
		Task<bool> DeleteLocalContact(string email);
		Task<bool> CheckIfExists(string email);
	}
}

