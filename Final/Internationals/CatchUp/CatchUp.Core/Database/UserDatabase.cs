using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CatchUp.Core.Interfaces;
using CatchUp.Core.Models;
using MvvmCross.Platform;
using SQLite.Net;

namespace CatchUp.Core
{
	public class UserDatabase
	{
		private SQLiteConnection database;
		public UserDatabase()
		{
			var sqlite = Mvx.Resolve<ISqlite>();
			database = sqlite.GetConnection();
			database.CreateTable<User>();
		}

		public Task AddCoffee(bool madeAtHome)
		{
			Debug.WriteLine("Add Coffee, LocalUSERDB:"+madeAtHome);
			throw new NotImplementedException();
		}

		public Task<IEnumerable<User>> GetCoffees()
		{
			Debug.WriteLine("Get Coffees, LocalUSERDB");
			throw new NotImplementedException();
		}

		public Task SyncCoffee()
		{
			Debug.WriteLine("Sync Coffee , LocalUSERDB:");
			throw new NotImplementedException();
		}
	}
}

