using SimpleChatApp.Models;
using SimpleChatWebApi.Handlers;
using System.Collections.Generic;
using System.Web.Http;

namespace SimpleChatWebApi.Controllers
{
    /// <summary>
    /// For operating with stored messages
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [Authorize]
    public class LogController : ApiController
    {
        /// <summary>
        /// Gets list of stored messages
        /// </summary>
        /// <returns>List of MessageModel</returns>
        [HttpGet]
        public List<MessageModel> List()
        {
            return AppVariables.Messages;
        }
    }
}
