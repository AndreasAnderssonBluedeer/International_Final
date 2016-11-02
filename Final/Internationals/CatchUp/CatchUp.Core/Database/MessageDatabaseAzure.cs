using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CatchUp.Core.Interfaces;
using CatchUp.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCross.Platform;

namespace CatchUp.Core.Database
{
	public class MessageDatabaseAzure: IMessageDatabase
	{
		private MobileServiceClient azureDatabase;
		private IMobileServiceSyncTable<Message> azureSyncTable;
		public MessageDatabaseAzure()
		{
			azureDatabase = Mvx.Resolve<IAzureMessageDatabase>().GetMobileServiceClient();
			azureSyncTable = azureDatabase.GetSyncTable<Message>();        
		}

		public async Task<int> CreateMessage(string id,string sender, string text, string locCord, double latitude, double longitude, string locText)
		{
			Message msg = new Message(id,sender,text,locCord, latitude, longitude, locText);
			await SyncAsync(true);
			await azureSyncTable.InsertAsync(msg);
			await SyncAsync();
			return 1;
		}

		public async Task<IEnumerable<Message>> GetMessages(string chatID)
		{
			await SyncAsync(true);
			var messages = await azureSyncTable.Where(x => x.ChatID == chatID).ToListAsync();
			return messages;
		}

		private async Task SyncAsync(bool pullData = false)
		{
			try
			{
				await azureDatabase.SyncContext.PushAsync();

				if (pullData)
				{
					await azureSyncTable.PullAsync("allMessages", azureSyncTable.CreateQuery()); // query ID is used for incremental sync
				}
			}

			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}
	}
}

