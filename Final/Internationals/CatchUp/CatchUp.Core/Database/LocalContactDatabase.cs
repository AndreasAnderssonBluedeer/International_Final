using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatchUp.Core.Interfaces;
using CatchUp.Core.Models;
using MvvmCross.Platform;
using SQLite.Net;

namespace CatchUp.Core.Database
{
	public class LocalContactDatabase : ILocalContactDatabase
	{
		
		private SQLiteConnection database;
		public LocalContactDatabase()
		{
			var sqlite = Mvx.Resolve<ISqlite>();
			database = sqlite.GetConnection();
			database.CreateTable<LocalContact>();
			var values = database.Table<LocalContact>().ToList();

		}
		public async Task<LocalContact> GetLocalContact(string email)
		{
			IEnumerable<LocalContact> list =  database.Table<LocalContact>().Where(x => x.EmailContact == email).ToList();
			return list.First();
		}

		public async Task<IEnumerable<LocalContact>> GetLocalContacts()
		{
			return database.Table<LocalContact>().ToList();
		}


		public async Task<int> CreateLocalContact(string email, string firstname, string lastname)
		{
			//If contact doesn't exist. MIGHT NOT WORK PROPERLY
		//	var obj = GetLocalContact(email).Result;
		//	   if ( obj.Email == null)
		//	{
				LocalContact uc = new LocalContact(email, firstname, lastname);
				var num = database.Insert(uc);
				database.Commit();
				return num;
		//	}
		//	else {
		//		return -1;
		//	}
		}

		public async Task<bool> DeleteLocalContact(string email)
		{
			var delete = database.Table<LocalContact>().Where(c => c.EmailContact == email).SingleOrDefault();
			database.Delete(delete);

			var control = database.Table<LocalContact>().Where(c => c.EmailContact == email).SingleOrDefault();
			return control == null;
		}

		public async Task<IEnumerable<LocalContact>> SearchContacts(string searchTerm)
		{
			return database.Table<LocalContact>().Where(c => c.EmailContact.Contains(searchTerm) ||
			                                            c.FirstNameContact.Contains(searchTerm) || 
			                                            c.LastNameContact.Contains(searchTerm));
		}

		public async Task<bool> CheckIfExists(string email)
		{
			return database.Table<LocalContact>().Any(x => x.EmailContact == email);
		}
	}
}

