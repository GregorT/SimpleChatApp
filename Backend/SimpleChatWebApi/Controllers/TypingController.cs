using SimpleChatApp.Models;
using SimpleChatWebApi.Hubs;
using System.Web.Http;

namespace SimpleChatWebApi.Controllers
{
    /// <summary>
    /// Controller for user typing status
    /// </summary>
    /// <seealso cref="SimpleChatWebApi.Controllers.ApiHubController{SimpleChatWebApi.Hubs.ChatHub}" />
    public class TypingController : ApiHubController<ChatHub>
    {
        /// <summary>
        /// User the typing status.
        /// </summary>
        /// <param name="status">The status.</param>
        [HttpPost]
        public void UserTyping(StatusModel status)
        {
            Hub.Clients.All.userTyping(status);
        }
    }
}
