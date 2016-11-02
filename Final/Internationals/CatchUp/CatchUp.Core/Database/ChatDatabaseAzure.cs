using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using CatchUp.Core;
using Microsoft.WindowsAzure.MobileServices;
//using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using CatchUp.Core.Interfaces;

using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using CatchUp.Core.Models;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MvvmCross.Platform;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CatchUp.Core.Database
{
    public class ChatDatabaseAzure : IChatDatabase
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<Chat> azureSyncTable;
        public ChatDatabaseAzure()
        {
            azureDatabase = Mvx.Resolve<IAzureChatDatabase>().GetMobileServiceClient();
            azureSyncTable = azureDatabase.GetSyncTable<Chat>();
        }

        public async Task<bool> CheckIfExists(string user1, string user2)
        {
            await SyncAsync(true);
            var exists = await azureSyncTable.Where(x => (x.User1 == user1 || x.User2 == user1)
                                                   && (x.User1 == user2 || x.User2 == user2)).ToListAsync();
            return exists.Any();
        }

        public async Task<string> GetChatID(string user1, string user2)
        {
            await SyncAsync(true);
            var exists = await azureSyncTable.Where(x => (x.User1 == user1 || x.User2 == user1)
                                                   && (x.User1 == user2 || x.User2 == user2)).ToListAsync();
            return exists.First().Id;
        }


        public async Task<int> CreateChat(string user1, string user2)
        {
            Chat chat = new Chat(user1, user2);
            await SyncAsync(true);
            await azureSyncTable.InsertAsync(chat);
            await SyncAsync();
            return 1;
        }

        public async Task<Chat> GetChat(string id)
        {
            await SyncAsync(true);
            IEnumerable<Chat> user = await azureSyncTable.Where(x => x.Id == id).ToListAsync();
            return user.First();
        }
        public async Task<IEnumerable<Chat>> GetChats(string user)
        {
            await SyncAsync(true);
            var chats = await azureSyncTable.Where(x => x.User1 == user ||
                                                   x.User2 == user).ToListAsync();
            return chats;
        }
        public async Task<IEnumerable<Chat>> GetUnreadChats(string user)
        {
            await SyncAsync(true);
            var chats = await azureSyncTable.Where(x => (x.User1 == user && !x.User1Read) ||
                                                   (x.User2 == user && !x.User2Read)).ToListAsync();
            return chats;
        }
        public async Task<IEnumerable<Chat>> GetReadChats(string user)
        {
            await SyncAsync(true);
            var chats = await azureSyncTable.Where(x => (x.User1 == user && x.User1Read) ||
                                                   (x.User2 == user && x.User2Read)).ToListAsync();
            return chats;
        }

        public async Task<bool> SetReadUser1(bool read, string id)
        {
            Chat chat = await GetChat(id);
            chat.User1Read = read;
            await SyncAsync(true);
            await azureSyncTable.UpdateAsync(chat);
            await SyncAsync();
            return true;

        }

        public async Task<bool> SetReadUser2(bool read, string id)
        {
            Chat chat = await GetChat(id);
            chat.User2Read = read;
            await SyncAsync(true);
            await azureSyncTable.UpdateAsync(chat);
            await SyncAsync();
            return true;
        }


        private async Task SyncAsync(bool pullData = false)
        {
            try
            {
                await azureDatabase.SyncContext.PushAsync();

                if (pullData)
                {
                    await azureSyncTable.PullAsync("allChats", azureSyncTable.CreateQuery()); // query ID is used for incremental sync
                }
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}


