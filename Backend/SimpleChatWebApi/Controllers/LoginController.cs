using SimpleChatApp.Models;
using SimpleChatWebApi.Handlers;
using SimpleChatWebApi.Hubs;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleChatWebApi.Controllers
{
    /// <summary>
    /// Controller for "Logging In"
    /// </summary>
    /// <seealso cref="SimpleChatWebApi.Controllers.ApiHubController{SimpleChatWebApi.Hubs.ChatHub}" />
    public class LoginController : ApiHubController<ChatHub>
    {
        /// <summary>
        /// Logins the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>UserModel & OK status if login was ok, otherwise 403 Error code</returns>
        [HttpPost]
        public HttpResponseMessage Login([FromBody]string username)
        {
            var user = new UserModel {
                Id = Guid.NewGuid(),
                Name = username
            };
            if (AppVariables.Users.Count >= 20)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            }
            AppVariables.Users.Add(user);
            Hub.Clients.All.userJoined(user);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }
}
