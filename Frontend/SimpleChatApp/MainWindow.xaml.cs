using Microsoft.AspNet.SignalR.Client;
using RestSharp;
using SimpleChatApp.Models;
using System;
using System.Linq;
using System.Windows;

namespace SimpleChatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            AppVariables.restClient = new RestClient($"{Properties.Settings.Default.ChatServiceUrl}/api");
            this.Content = new pgLogin();
        }

        /// <summary>
        /// Connects to hub.
        /// </summary>
        public void ConnectToHub()
        {
            var connection = new HubConnection(Properties.Settings.Default.ChatServiceUrl);
            var proxy = connection.CreateHubProxy("ChatHub");
            AppVariables.hubConnection = connection;
            AppVariables.hubProxy = proxy;
            AppVariables.hubConnection.Start().Wait();

            //invocation when message is sent
            AppVariables.hubProxy.On<MessageModel>("sendMessage", p =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    AppVariables.Messages.Add(p);
                }));
            });

            //invocation when user joined
            AppVariables.hubProxy.On<UserModel>("userJoined", p =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    var viewmodel = new UserViewModel
                    {
                        Action = "",
                        Id = p.Id,
                        Name = p.Name
                    };
                    AppVariables.UserList.Add(viewmodel);
                }));
            });

            //invocation when user left
            AppVariables.hubProxy.On<UserModel>("userLeft", p =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    var item = AppVariables.UserList.Single(u => u.Id == p.Id);
                    AppVariables.UserList.Remove(item);
                }));
            });

            //invocation when user's status is changed
            AppVariables.hubProxy.On<StatusModel>("userTyping", p =>
            {
                App.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    if (p.IsIdle)
                    {
                        var item = AppVariables.UserList.Single(u => u.Id == p.UserId);
                        item.Action = "";
                    }
                    else
                    {
                        var item = AppVariables.UserList.Single(u => u.Id == p.UserId);
                        item.Action = "typing";
                    }
                }));
            });
        }

        /// <summary>
        /// Handles the Closed event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (AppVariables.CurrentUser != null)
            {
                var request = new RestSharp.RestRequest("logout", RestSharp.Method.POST);
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddBody(AppVariables.CurrentUser);
                var response = AppVariables.restClient.Execute(request);
                if (AppVariables.hubConnection.State == ConnectionState.Connected) AppVariables.hubConnection.Stop();
                AppVariables.CurrentUser = null;
            }
        }
    }
}