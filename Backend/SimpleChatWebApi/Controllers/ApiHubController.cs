using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Web.Http;

namespace SimpleChatWebApi.Controllers
{
    /// <summary>
    /// Abtracted class to implement SignalR hub into api controller
    /// </summary>
    /// <typeparam name="THub">The type of the hub.</typeparam>
    /// <seealso cref="System.Web.Http.ApiController" />
    public abstract class ApiHubController<THub> : ApiController where THub : IHub
    {
        Lazy<IHubContext> hub = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<THub>());

        /// <summary>
        /// Gets the hub.
        /// </summary>
        /// <value>
        /// The hub.
        /// </value>
        protected IHubContext Hub
        {
            get { return hub.Value; }
        }
    }
}
