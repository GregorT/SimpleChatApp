using Microsoft.AspNet.SignalR;
using SimpleChatApp.Models;
using SimpleChatService.BusinessLogic;
using System;
using System.Collections.Generic;

namespace SimpleChatService
{
    public class ChatHub : Hub
    {
        private readonly LoginHandler _loginHandler = new LoginHandler();
        public void Send(MessageModel message)
        {
            Clients.All.sendMessage(message);
        }

        private void UserJoined(UserModel user)
        {
            Clients.Others.userJoined(user);
        }

        private void UserLeft(UserModel user)
        {
            Clients.Others.userLeft(user);
        }

        public void UserTyping(Guid id, bool isIdle = true)
        {
            Clients.Others.userTyping(id, isIdle);
        }

        public void ActiveUsers()
        {
            //todo: get the list from database
            var userList = new List<UserModel>();
            Clients.Caller.listUsers(userList);
        }

        public void MessageLog()
        {
            //todo: get the list of max last 20 messages
            var messageList = new List<MessageModel>();
            Clients.Caller.listMessages(messageList);
        }

        public void LogIn(string username)
        {
            var result = _loginHandler.Login(username);
            Clients.Caller.userLogin(result);
            if (result != null) UserJoined(result);
        }

        public void LogOut(UserModel user)
        {
            _loginHandler.Logout(user);
            UserLeft(user);
        }
    }
}
