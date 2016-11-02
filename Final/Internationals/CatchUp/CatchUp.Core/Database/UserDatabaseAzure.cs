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
    public class UserDatabaseAzure : IUserDatabase  //change to iUserdb
    {
        private MobileServiceClient azureDatabase;
        private IMobileServiceSyncTable<User> azureSyncTable;
        public UserDatabaseAzure()
        {
            azureDatabase = Mvx.Resolve<IAzureDatabase>().GetMobileServiceClient();
            azureSyncTable = azureDatabase.GetSyncTable<User>();        //User datamodel
            Debug.WriteLine("WOHOOO" + azureDatabase.SyncContext.IsInitialized);
        }

        public async Task<int> CreateUser(string email, string firstName, string lastName)
        {

            User user = new User(email, firstName, lastName);
            await SyncAsync(true);
            //	if (CheckIfExists(email).Result == false)
            //	{
            await azureSyncTable.InsertAsync(user);
            //	}
            //	else {
            //		await SyncAsync();
            //		return 0;
            //	}
            await SyncAsync();
            return 1;
        }

        public async Task<IEnumerable<User>> GetUsers(string searchTerm)
        {
            await SyncAsync(true);
            var users = await azureSyncTable.Where(x => x.Email.Contains(searchTerm) ||
                                                   x.LastName.Contains(searchTerm) || x.FirstName.Contains(searchTerm)).ToListAsync();
            return users;
        }


        public async Task<bool> CheckIfExists(string email)
        {
            await SyncAsync(true);
            var users = await azureSyncTable.Where(x => x.Email == email).ToListAsync();
            return users.Any();
        }
        public async Task<User> GetUser(string email)
        {
            await SyncAsync(true);
            IEnumerable<User> users = await azureSyncTable.Where(x => x.Email == email).ToListAsync();
            return users.First();
        }

        private async Task SyncAsync(bool pullData = false)
        {
            try
            {
                await azureDatabase.SyncContext.PushAsync();

                if (pullData)
                {
                    await azureSyncTable.PullAsync("allUsers", azureSyncTable.CreateQuery()); // query ID is used for incremental sync
                }
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        public async Task<bool> UpdateFirstName(string mail, string name)
        {
            IEnumerable<User> u = await azureSyncTable.Where(x => x.Email == mail).ToListAsync();
            User user = u.First();
            user.FirstName = name;
            await SyncAsync(true);
            await azureSyncTable.UpdateAsync(user);
            await SyncAsync();
            return true;
        }
        public async Task<bool> UpdateLastName(string mail, string name)
        {
            IEnumerable<User> u = await azureSyncTable.Where(x => x.Email == mail).ToListAsync();
            User user = u.First();
            user.LastName = name;
            await SyncAsync(true);
            await azureSyncTable.UpdateAsync(user);
            await SyncAsync();
            return true;
        }
    }
}

