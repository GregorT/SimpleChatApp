using SimpleChatApp.Models;
using SimpleChatWebApi.Data;
using SimpleChatWebApi.Handlers;
using SimpleChatWebApi.Hubs;
using System.Web.Http;

namespace SimpleChatWebApi.Controllers
{
    /// <summary>
    /// Controller for sending messages
    /// </summary>
    /// <seealso cref="SimpleChatWebApi.Controllers.ApiHubController{SimpleChatWebApi.Hubs.ChatHub}" />
    public class MessageController : ApiHubController<ChatHub>
    {
        /// <summary>
        /// Sends the message to the hub and stores it.
        /// </summary>
        /// <param name="message">The message.</param>
        [HttpPost]
        public void Send(MessageModel message)
        {
            AppVariables.Messages.Add(message);
            using (var context = new ChatDbContext())
            {
                context.Messages.Add(new MessageLog { DatePosted = message.Posted, Message = message.Message, UserId = message.UserId, UserName = message.Name });
                context.SaveChanges();
            }
            Hub.Clients.All.sendMessage(message);
            var msgCount = AppVariables.Messages.Count;
            if (msgCount > 20) AppVariables.Messages.RemoveRange(0, msgCount - 20);
        }
    }
}
