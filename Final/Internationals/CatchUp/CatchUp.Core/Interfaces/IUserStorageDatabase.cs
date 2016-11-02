using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchUp.Core.Interfaces
{
	public interface IUserStorageDatabase
	{

		//Return settings & user info
		Task<string> GetEmail();
		Task<string> GetFirstName();
		Task<string> GetLastName();
		Task<string> GetAutoMessage();

		Task<bool> GetSound();
		Task<bool> GetVibration();
		Task<bool> GetMode();

		//Return bool to indicate success. For updating info
		Task<bool> UpdateEmail(string email);
		Task<bool> UpdateFirstName(string firstname);
		Task<bool> UpdateLastName(string lastname);
		Task<bool> UpdateAutoMessage(string message);
		Task<bool> UpdateSound(bool sound);
		Task<bool> UpdateVibration(bool vibration);
		Task<bool> UpdateMode(bool mode);
		Task<bool> UserExists();

		//Return int id to indicate success, Creates a user and saves to local storage.
		Task<int> CreateUser(string email, string firstname, string lastname);

	}
}

