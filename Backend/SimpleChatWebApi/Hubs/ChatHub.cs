using Microsoft.AspNet.SignalR;
using SimpleChatWebApi.Handlers;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleChatWebApi.Hubs
{
    /// <summary>
    /// SignalR hub for chat
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    public class ChatHub : Hub
    {
        /// <summary>
        /// Called when the connection connects to this hub instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" />
        /// </returns>
        public override Task OnConnected()
        {
            var identity = Context.User.Identity as ClaimsIdentity;
            var claim = identity.Claims.FirstOrDefault(p => p.Type == "user_name");
            var userName = claim.Value;
            var user = AppVariables.Users.Single(p => p.User.Name == userName);
            user.ConnectionId = Context.ConnectionId;
            return Clients.Others.userJoined(user.User);
        }

        /// <summary>
        /// Called when a connection disconnects from this hub gracefully or due to a timeout.
        /// </summary>
        /// <param name="stopCalled">true, if stop was called on the client closing the connection gracefully;
        /// false, if the connection has been lost for longer than the
        /// <see cref="P:Microsoft.AspNet.SignalR.Configuration.IConfigurationManager.DisconnectTimeout" />.
        /// Timeouts can be caused by clients reconnecting to another SignalR server in scaleout.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" />
        /// </returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var identity = Context.User.Identity as ClaimsIdentity;
            var claim = identity.Claims.FirstOrDefault(p => p.Type == "user_name");
            var userName = claim.Value;
            var user = AppVariables.Users.Single(p => p.User.Name == userName);
            AppVariables.Users.Remove(user);
            return Clients.Others.userLeft(user);
        }
    }
}