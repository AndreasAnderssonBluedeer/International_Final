using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CatchUp.Core.Interfaces;
using CatchUp.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCross.Platform;

namespace CatchUp.Core
{
	public class ContactRequestDatabaseAzure : IContactRequestDatabase
	{
		private MobileServiceClient azureDatabase;
		private IMobileServiceSyncTable<ContactRequest> azureSyncTable;
		public ContactRequestDatabaseAzure()
		{
			azureDatabase = Mvx.Resolve<IAzureContactRequestDatabase>().GetMobileServiceClient();
			azureSyncTable = azureDatabase.GetSyncTable<ContactRequest>();
		}

		public async Task<int> AnswerRequest(string id, bool answer)
		{
			await SyncAsync(true);
			var req = await azureSyncTable.Where(x => x.Id == id).ToListAsync();
			ContactRequest res = req.FirstOrDefault();
			res.AnsweredRequest = true;
			res.Accepted = answer;
			await azureSyncTable.UpdateAsync(res);
			await SyncAsync();
			return 1;
		}

		public async Task<int> CreateContactRequest(string sender, string senderFirstName, string senderLastName, string receiver)
		{
			ContactRequest req = new ContactRequest(sender, senderFirstName, senderLastName, receiver);
			await SyncAsync(true);
			await azureSyncTable.InsertAsync(req);
			await SyncAsync();
			return 1;
		}

		public async Task<int> DeleteRequest(string id)
		{
			await SyncAsync(true);
			var req = await azureSyncTable.Where(x => x.Id == id).ToListAsync();
			if (req.Any())
			{
				await azureSyncTable.DeleteAsync(req.FirstOrDefault());
				await SyncAsync();
				return 1;
			}
			else
			{
				return 0;

			}
		}

		public async Task<IEnumerable<ContactRequest>> GetAnsweredRequests(string sender)
		{
			await SyncAsync(true);
			var reqs = await azureSyncTable.Where(x => x.Sender == sender && x.AnsweredRequest == true).ToListAsync();
			return reqs;
		}

		public async Task<IEnumerable<ContactRequest>> GetRequests(string receiver)
		{
			await SyncAsync(true);
			var reqs = await azureSyncTable.Where(x => x.Receiver == receiver && x.AnsweredRequest == false).ToListAsync();
			return reqs;
		}

		public async Task<bool> CheckIfExists(string sender, string receiver)
		{
			await SyncAsync(true);
			var request = await azureSyncTable.Where(x => (x.Sender == sender && x.Receiver == receiver) || (x.Sender == receiver && x.Receiver == sender)).ToListAsync();
			return request.Any();
		}



		private async Task SyncAsync(bool pullData = false)
		{
			try
			{
				await azureDatabase.SyncContext.PushAsync();

				if (pullData)
				{
					await azureSyncTable.PullAsync("allContactRequests", azureSyncTable.CreateQuery()); // query ID is used for incremental sync
				}
			}

			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}
	}
}

