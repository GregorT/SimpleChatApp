using System;
using Microsoft.AspNet.SignalR;
using SimpleChatApp.Models;

namespace SimpleChatWebApi.Hubs
{
    /// <summary>
    /// SignalR hub for chat
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    public class ChatHub : Hub
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(MessageModel message)
        {
            Clients.All.sendMessage(message);
        }

        /// <summary>
        /// Users the joined.
        /// </summary>
        /// <param name="user">The user.</param>
        private void UserJoined(UserModel user)
        {
            Clients.Others.userJoined(user);
        }

        /// <summary>
        /// Users the left.
        /// </summary>
        /// <param name="user">The user.</param>
        private void UserLeft(UserModel user)
        {
            Clients.Others.userLeft(user);
        }

        /// <summary>
        /// Users the typing.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="isIdle">if set to <c>true</c> [is idle].</param>
        public void UserTyping(Guid id, bool isIdle = true)
        {
            Clients.Others.userTyping(id, isIdle);
        }

    }
}