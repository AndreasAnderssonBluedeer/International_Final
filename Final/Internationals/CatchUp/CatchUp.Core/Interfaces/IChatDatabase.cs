using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatchUp.Core.Models;

namespace CatchUp.Core.Interfaces
{
    public interface IChatDatabase
    {

        Task<int> CreateChat(string user1, string user2);
        Task<Chat> GetChat(string id);
        Task<IEnumerable<Chat>> GetChats(string user);
        Task<IEnumerable<Chat>> GetUnreadChats(string user);
        Task<IEnumerable<Chat>> GetReadChats(string user);
        Task<bool> SetReadUser1(bool read, string id);
        Task<bool> SetReadUser2(bool read, string id);
        Task<bool> CheckIfExists(string user1, string user2);
        Task<string> GetChatID(string user1, string user2);

    }
}

