using Microsoft.AspNet.SignalR.Client;
using RestSharp;
using SimpleChatApp.Models;
using System.Collections.ObjectModel;

namespace SimpleChatApp
{
    /// <summary>
    /// Static application variables
    /// </summary>
    public static class AppVariables
    {
        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public static UserModel CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the hub connection.
        /// </summary>
        /// <value>
        /// The hub connection.
        /// </value>
        public static HubConnection hubConnection { get; set; }

        /// <summary>
        /// Gets or sets the hub proxy.
        /// </summary>
        /// <value>
        /// The hub proxy.
        /// </value>
        public static IHubProxy hubProxy { get; set; }

        /// <summary>
        /// Gets or sets the rest client.
        /// </summary>
        /// <value>
        /// The rest client.
        /// </value>
        public static RestClient restClient { get; set; }

        /// <summary>
        /// Gets or sets the user list.
        /// </summary>
        /// <value>
        /// The user list.
        /// </value>
        public static ObservableCollection<UserViewModel> UserList { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public static ObservableCollection<MessageModel> Messages { get; set; }
    }
}
