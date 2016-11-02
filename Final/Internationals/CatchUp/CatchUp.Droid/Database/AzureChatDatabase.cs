using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CatchUp.Core;
using Microsoft.WindowsAzure.MobileServices;
//using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using CatchUp.Core.Interfaces;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using CatchUp.Core.Models;

namespace CatchUp.Droid
{
	public class AzureChatDatabase: IAzureChatDatabase
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
			var sqliteFilename = "ChatSQLite.db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}
			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<Core.Models.Chat>();
			azureDatabase.SyncContext.InitializeAsync(store);
		}
	}
}


