using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatchUp.Core.Models;

namespace CatchUp.Core.Interfaces
{
	public interface IContactRequestDatabase
	{
		
		Task<int> CreateContactRequest(string sender, string senderFirstName, 
		                                   string senderLastName,string receiver);

		Task<int> DeleteRequest(string id);
		Task<IEnumerable<ContactRequest>> GetRequests(string receiver);
		Task<int> AnswerRequest(string id,bool answer);
		Task<IEnumerable<ContactRequest>> GetAnsweredRequests(string sender);
		Task<bool> CheckIfExists(string sender, string receiver);
	}
}

