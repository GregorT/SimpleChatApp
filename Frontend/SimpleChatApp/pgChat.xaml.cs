using Microsoft.AspNet.SignalR.Client;
using SimpleChatApp.Models;
using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SimpleChatApp
{
    /// <summary>
    /// Interaction logic for pgChat.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Page" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class pgChat : Page
    {
        private DateTime? LastTyped { get; set; }
        private readonly DispatcherTimer idleTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="pgChat"/> class.
        /// </summary>
        public pgChat()
        {
            InitializeComponent();
            lstUsers.ItemsSource = AppVariables.UserList;
            lstMessages.ItemsSource = AppVariables.Messages;
            ((INotifyCollectionChanged)lstMessages.Items).CollectionChanged += lstMessages_CollectionChanged;
            if (lstMessages.Items.Count > 2)
            {
                lstMessages.ScrollIntoView(lstMessages.Items[lstMessages.Items.Count - 1]);
            }
            idleTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2),
                IsEnabled = true
            };
            idleTimer.Tick += IdleTimer_Tick;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the lstMessages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void lstMessages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            lstMessages.ScrollIntoView(e.NewItems[0]);
        }

        /// <summary>
        /// Handles the Tick event of the IdleTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            if (LastTyped == null) return;
            LastTyped = null;
            var typeRequest = new RestSharp.RestRequest("typing", RestSharp.Method.POST);
            var status = new StatusModel
            {
                IsIdle = true,
                UserId = AppVariables.Username
            };
            typeRequest.RequestFormat = RestSharp.DataFormat.Json;
            typeRequest.AddHeader("Authorization", $"Bearer {AppVariables.Token}");
            typeRequest.AddObject(status);
            var typeResponse = AppVariables.restClient.Execute(typeRequest);
        }

        /// <summary>
        /// Handles the KeyDown event of the txtMessage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            idleTimer.Stop();
            if (e.Key != Key.Enter)
            {
                if (LastTyped == null)
                {
                    LastTyped = DateTime.Now;
                    var typeRequest = new RestSharp.RestRequest("typing", RestSharp.Method.POST);
                    typeRequest.AddHeader("Authorization", $"Bearer {AppVariables.Token}");
                    var status = new StatusModel
                    {
                        IsIdle = false,
                        UserId = AppVariables.Username
                    };
                    typeRequest.RequestFormat = RestSharp.DataFormat.Json;
                    typeRequest.AddObject(status);
                    var typeResponse = AppVariables.restClient.Execute(typeRequest);
                }
                return;
            }
            if (string.IsNullOrEmpty(txtMessage.Text)) return;
            var request = new RestSharp.RestRequest("message", RestSharp.Method.POST);
            request.AddHeader("Authorization", $"Bearer {AppVariables.Token}");
            var msg = new MessageModel
            {
                Message = txtMessage.Text,
                Name = AppVariables.Username,
                Posted = DateTime.Now,
                UserId = AppVariables.Username
            };
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddObject(msg);
            var response = AppVariables.restClient.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent) txtMessage.Text = "";
        }

        /// <summary>
        /// Handles the KeyUp event of the txtMessage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtMessage_KeyUp(object sender, KeyEventArgs e)
        {
            idleTimer.Start();
        }

        /// <summary>
        /// Handles the Click event of the mnuDisconnect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void mnuDisconnect_Click(object sender, RoutedEventArgs e)
        {
            if (AppVariables.hubConnection.State== ConnectionState.Connected) AppVariables.hubConnection.Stop();
            AppVariables.Token = null;
            var main = (MainWindow)Parent;
            main.Content = new pgLogin();
        }
    }
}