using SimpleChatApp.Models;
using SimpleChatWebApi.Models;
using System.Collections.Generic;

namespace SimpleChatWebApi.Handlers
{
    /// <summary>
    /// WebAPI static items
    /// </summary>
    public static class AppVariables
    {
        /// <summary>
        /// The users
        /// </summary>
        public static List<UserServerModel> Users = new List<UserServerModel>();

        /// <summary>
        /// The messages
        /// </summary>
        public static List<MessageModel> Messages = new List<MessageModel>();
    }
}