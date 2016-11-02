using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CatchUp.Core.Interfaces;
using MvvmCross.Platform;
using SQLite.Net;

namespace CatchUp.Core.Database
{
	
public class UserStorageDatabase : IUserStorageDatabase
		{
			private SQLiteConnection database;
			public UserStorageDatabase()
			{
				
				var sqlite = Mvx.Resolve<ISqlite>();
				database = sqlite.GetConnection();
				database.CreateTable<UserStorage>();
			}

		public async Task<int> CreateUser(string email, string firstname, string lastname)
		{
			//Control if user exist against Azure
			UserStorage userStorage = new UserStorage(email, firstname, lastname);
			userStorage.AutoMessage = "Sorry, i'm busy at the moment, i'll come back to you later.";
			var num = database.Insert(userStorage);
			database.Commit();

			return num;
		}

		public async Task<string> GetAutoMessage()
		{
			var value = database.Table<UserStorage>().ToList();
			return value.First().AutoMessage;
		}

		public async Task<string> GetEmail()
		{
			var value = database.Table<UserStorage>().ToList();
			return value.First().Email;
		}

		public async Task<string> GetFirstName()
		{
			var value = database.Table<UserStorage>().ToList();
			return value.First().FirstName;
		}

		public async Task<string> GetLastName()
		{
			var value = database.Table<UserStorage>().ToList();
			return value.First().LastName;
		}

		public async Task<bool> GetMode()
		{
			var value = database.Table<UserStorage>().ToList();
			return value.First().Mode;
		}

		public async Task<bool> GetSound()
		{
			
			var value = database.Table<UserStorage>().ToList();
			return value.First().Sound;
		}

		public async Task<bool> GetVibration()
		{
			var value = database.Table<UserStorage>().ToList();
			return value.First().Vibration;
		}

		public async Task<bool> UpdateAutoMessage(string message)
		{
			var query = database.Table<UserStorage>().Where(c => c.Id == 1).SingleOrDefault();
			query.AutoMessage = message;
			database.Update(query);

			var value = database.Table<UserStorage>().Any(x => x.AutoMessage ==message);
			return value;
		}

		public async Task<bool> UpdateEmail(string email)
		{
			var query = database.Table<UserStorage>().Where(c => c.Id == 1).SingleOrDefault();
			query.Email = email;
			database.Update(query);

			var value = database.Table<UserStorage>().Any(x => x.Email == email);
			return value;
		}

		public async Task<bool> UpdateFirstName(string firstname)
		{
			var query = database.Table<UserStorage>().Where(c => c.Id == 1).SingleOrDefault();
			query.FirstName = firstname;
			database.Update(query);

			var value = database.Table<UserStorage>().Any(x => x.FirstName == firstname);
			return value;
		}

		public async Task<bool> UpdateLastName(string lastname)
		{
			var query = database.Table<UserStorage>().Where(c => c.Id == 1).SingleOrDefault();
			query.LastName = lastname;
			database.Update(query);

			var value = database.Table<UserStorage>().Any(x => x.LastName == lastname);
			return value;
		}

		public async Task<bool> UpdateMode(bool mode)
		{
			var query = database.Table<UserStorage>().Where(c => c.Id == 1).SingleOrDefault();
			query.Mode = mode;
			database.Update(query);

			var value = database.Table<UserStorage>().Any(x => x.Mode == mode);
			return value;
		}

		public async Task<bool> UpdateSound(bool sound)
		{
			var query = database.Table<UserStorage>().Where(c => c.Id == 1).SingleOrDefault();
			query.Sound = sound;
			database.Update(query);

			var value = database.Table<UserStorage>().Any(x => x.Sound == sound);
			return value;
		}

		public async Task<bool> UpdateVibration(bool vibration)
		{
			var query = database.Table<UserStorage>().Where(c => c.Id == 1).SingleOrDefault();
			query.Vibration = vibration;
			database.Update(query);

			var value = database.Table<UserStorage>().Any(x => x.Vibration == vibration);
			return value;
		}

		public async Task<bool> UserExists()
		{
			
			var value = database.Table<UserStorage>().Any();
 			var values = database.Table<UserStorage>().ToList();
			return value;

		}

	}
}

