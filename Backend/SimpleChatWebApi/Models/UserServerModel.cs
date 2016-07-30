using SimpleChatApp.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace SimpleChatWebApi.Models
{
    public class UserServerModel
    {
        public string ConnectionId { get; set; }
        public UserModel User { get; set; }
    }
}