using SimpleChatApp.Models;
using System;
using System.Linq;

namespace SimpleChatService.BusinessLogic
{
    internal class LoginHandler
    {
        public UserModel Login(string username)
        {
            if (AppVariables.Users.Count >= 20) return null;
            if (AppVariables.Users.Count(p => p.Name == username) != 0) return null;
            var user = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = username
            };
            AppVariables.Users.Add(user);
            return user;
        }

        public void Logout(UserModel user)
        {
            AppVariables.Users.Remove(user);
        }
    }
}
