using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using System.Collections.Generic;
using CatchUp.Core.Models;
using System.Diagnostics;
using System.Collections;
using CatchUp.Core.Interfaces;
using System.Collections.ObjectModel;

// Author: Andreas Andersson n9795383, Marie-Luise Lux n9530801, Samuel Blight n8312885

namespace CatchUp.Core.ViewModels
{
    public class NotificationViewModel
        : MvxViewModel
    {

        private IChatDatabase chatdb;
        private IMessageDatabase msgdb;
        private IUserStorageDatabase uStoreDb;
        private string user;

        private ObservableCollection<Chat> unreadChats;
        public ObservableCollection<Chat> UnreadChats
        {
            get { return unreadChats; }
            set { SetProperty(ref unreadChats, value); }

        }

        private ObservableCollection<Chat> readChats;
        public ObservableCollection<Chat> ReadChats
        {
            get { return readChats; }
            set { SetProperty(ref readChats, value); }

        }

        private string user1;
        public string User1
        {
            get { return user1; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref user1, value);
                }
            }
        }
        private string user2;
        public string User2
        {
            get { return user2; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref user2, value);
                }
            }
        }

        private string user1Read;
        public string User1Read
        {
            get { return user1Read; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref user1Read, value);
                }
            }
        }

        public ICommand SelectChatCommand { get; private set; }
        public NotificationViewModel(IChatDatabase chatdb, IMessageDatabase msgdb, IUserStorageDatabase uStoreDb)
        {
            this.chatdb = chatdb;
            this.msgdb = msgdb;
            this.uStoreDb = uStoreDb;
            user = uStoreDb.GetEmail().Result;
            unreadChats = new ObservableCollection<Chat>();
            readChats = new ObservableCollection<Chat>();

            ShowChats();

            SelectChatCommand = new MvxCommand<Core.Models.Chat>(chat =>
            {
                SelectChat(chat);
            });
        }

        public async void SelectChat(Chat chat)
        {
            if (chat.User1 == user)
            {
                await chatdb.SetReadUser1(true, chat.Id);
            }
            else if (chat.User2 == user)
            {
                await chatdb.SetReadUser2(true, chat.Id);
            }

            ShowViewModel<MessageViewModel>(new
            {
                chatID = chat.Id,
                user1 = chat.User1,
                user2 = chat.User2,
                user = user
            });
        }

        public async void ShowChats()
        {
            //await chatdb.CreateChat(user, "TestNummer1");
            //await msgdb.CreateMessage(await chatdb.GetChatID(
            //    user, "TestNummer1"), user, "dies ist ein Text", "Coord", "Library 4th floor");
            //await chatdb.CreateChat("TestNummer2", user);
            //await msgdb.CreateMessage(await chatdb.GetChatID(
            //user, "TestNummer2"), user, "dies ist ein Text", "Coord", "Library 4th floor");

            var unreadList = await chatdb.GetUnreadChats(uStoreDb.GetEmail().Result);
            foreach (var u in unreadList)
            {
                if (user == u.User1)
                {
                    //await chatdb.SetReadUser1(true, u.Id);
                    u.User1 = u.User2;
                }
                unreadChats.Add(u);
                RaisePropertyChanged(() => UnreadChats);
            }

            var readList = await chatdb.GetReadChats(uStoreDb.GetEmail().Result);
            foreach (var u in readList)
            {
                if (user == u.User1)
                {
                    u.User1 = u.User2;
                }
                readChats.Add(u);
                RaisePropertyChanged(() => ReadChats);
            }
        }

    }
}

