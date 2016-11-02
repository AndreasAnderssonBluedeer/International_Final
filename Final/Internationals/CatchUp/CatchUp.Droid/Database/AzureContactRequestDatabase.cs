using System;
using CatchUp.Core;
using Microsoft.WindowsAzure.MobileServices;
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace CatchUp.Droid
{
	public class AzureContactRequestDatabase : IAzureContactRequestDatabase
	{
		MobileServiceClient azureDatabase;
		public MobileServiceClient GetMobileServiceClient()
		{
			CurrentPlatform.Init();

			azureDatabase = new MobileServiceClient("http://catchup5.azurewebsites.net");
			InitializeLocal();
			return azureDatabase;
		}
		private void InitializeLocal()
		{
			var sqliteFilename = "ContactRequestSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}
			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<Core.Models.ContactRequest>();
			azureDatabase.SyncContext.InitializeAsync(store);
		}
	}
}


