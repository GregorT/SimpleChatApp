using SimpleChatApp.Models;
using SimpleChatWebApi.Handlers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SimpleChatWebApi.Controllers
{
    /// <summary>
    /// the user list controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [Authorize]
    public class UsersController : ApiController
    {
        /// <summary>
        /// Lists active users.
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public List<UserModel> List()
        {
            return AppVariables.Users.Select(p=>p.User).ToList();
        }
    }
}
