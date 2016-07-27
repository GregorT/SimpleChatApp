using SimpleChatApp.Models;
using System.Collections.Generic;

namespace SimpleChatService
{
    internal static class AppVariables
    {
        public static List<UserModel> Users = new List<UserModel>();
        public static List<MessageModel> Messages = new List<MessageModel>();
    }
}
