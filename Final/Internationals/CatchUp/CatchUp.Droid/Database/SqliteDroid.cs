using System;
using System.IO;
using CatchUp.Core;
using CatchUp.Core.Interfaces;
using SQLite.Net;
//Local database
namespace CatchUp.Droid.Database
{
	public class SqliteDroid : ISqlite
	{
		// Call to the dependency service
		public SqliteDroid() { }
		public SQLiteConnection GetConnection()
		{
			// Specify Documents folder 
			//var sqliteFilename = "ContactSQLite.db3";
			var sqliteFilename = "UserStorageSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, sqliteFilename);
			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}
			// Create the connection
			var conn = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path);

			// Return the database connection
			return conn;

		}
	}
}