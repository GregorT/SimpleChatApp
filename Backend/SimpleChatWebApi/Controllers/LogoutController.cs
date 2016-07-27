using SimpleChatApp.Models;
using SimpleChatWebApi.Handlers;
using SimpleChatWebApi.Hubs;
using System.Linq;
using System.Web.Http;

namespace SimpleChatWebApi.Controllers
{
    /// <summary>
    /// Controller for user to log out of chat
    /// </summary>
    /// <seealso cref="SimpleChatWebApi.Controllers.ApiHubController{SimpleChatWebApi.Hubs.ChatHub}" />
    public class LogoutController : ApiHubController<ChatHub>
    {
        /// <summary>
        /// Logouts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        [HttpPost]
        public void Logout(UserModel user)
        {
            var item = AppVariables.Users.SingleOrDefault(p => p.Id == user.Id);
            if (item == null) return;
            AppVariables.Users.Remove(item);
            Hub.Clients.All.userLeft(user);
        }
    }
}
