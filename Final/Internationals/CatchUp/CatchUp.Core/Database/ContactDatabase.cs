using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatchUp.Core.Interfaces;
using CatchUp.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;
using SQLite.Net;

namespace CatchUp.Core.Database
{
	public class ContactDatabase : IContactDatabase
	{
		private SQLiteConnection database;
		public ContactDatabase()
		{
			var sqlite = Mvx.Resolve<ISqlite>();
			database = sqlite.GetConnection();
			database.CreateTable<Contact>();
		}

		public async Task<bool> AddContact(Contact contact)
		{
			 database.Insert(contact);
			database.Commit();
			//Check that the contact's added. 
			if (CheckIfExists(contact.Email).Result)
			{
				return true;
			}
			return false;
		}

		public async Task<bool> CheckIfExists(string email)
		{
			return database.Table<Contact>().Any(x => x.Email == email);
		}

		public async Task<bool> CreateContact(string firstName, string lastName, string email)
		{
			Contact contact = new Contact(firstName, lastName, email);
			database.Insert(contact);
			database.Commit();

			//Check that the contact's created. 
			if (CheckIfExists(email).Result)
			{
				return true;
			}
			return false;
		}

		public async Task<bool> DeleteContact(string email)
		{
			database.Delete<Contact>(email);
			database.Commit();
			//Check if the contact's deleted. 
			if ( !CheckIfExists(email).Result)
			{
				return true;
			}
			return false;
		}

		public async Task<IEnumerable<Contact>> GetAllContacts()
		{
			return database.Table<Contact>().ToList();
		}

		public async Task<Contact> GetContact(string email)
		{
			return database.Get<Contact>(email);
		}

		public async Task<IEnumerable<Contact>> SearchContacts(string email)
		{
			return null; //database.Table<Contact>().
		}



		public async Task<IQueryable<Contact>> GetPersonalContacts(string email)
		{

			return (System.Linq.IQueryable<CatchUp.Core.Models.Contact>)database.Query<Contact>
				                      ("SELECT Email FROM [Contact] WHERE [Email] LIKE " + email);

		

		}

		Task<IEnumerable<Contact>> IContactDatabase.GetPersonalContacts(string email)
		{
			throw new NotImplementedException();
		}
	}
}

