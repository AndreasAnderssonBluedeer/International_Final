using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatchUp.Core.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace CatchUp.Core.Interfaces
	{
		public interface IContactDatabase
		{
		//AddContact,GetAllContactsList, GetContactsList, GetContact, CreateContactProfile,EmailExists/ContactExists

		//Return list of all contacts existing
		Task<IEnumerable<Contact>> GetAllContacts();

		//Return list of contacts for a "Profile/Contact)
		Task<IEnumerable<Contact>> GetPersonalContacts(string email);

		//Return if contact is deleted or not
		Task<bool> DeleteContact(string email);

		//Return entire contact
		Task<Contact> GetContact(string email);

		//Return whether a contact exists or not
		Task<bool> CheckIfExists(String email);

		//Create Contact, returns if all went ok
		Task<bool> CreateContact(string firstName, string lastName, string email);

		//Add contact to personal contactList, return if all went ok
		Task<bool> AddContact(Contact contact);

		//****Message system for contact requests.**** 

	

		}
	}

