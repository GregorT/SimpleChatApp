using RestSharp;
using SimpleChatApp.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SimpleChatApp
{
    /// <summary>
    /// Interaction logic for pgLogin.xaml
    /// </summary>
    public partial class pgLogin : Page
    {
        public pgLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnConnect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim())) return;
            lblError.Content = "";
            var request = new RestRequest("login", Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(txtUsername.Text);
            try
            {
                var response = AppVariables.restClient.Execute<UserModel>(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    lblError.Content = "Sorry but we are full. Please do come back later.";
                    return;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Data.Name != null)
                {
                    AppVariables.CurrentUser = response.Data;
                    request = new RestRequest("users", Method.GET);
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    var userList = AppVariables.restClient.Execute<List<UserModel>>(request);
                    AppVariables.UserList = new System.Collections.ObjectModel.ObservableCollection<UserViewModel>();
                    
                    foreach (var item in userList.Data)
                    {
                        AppVariables.UserList.Add(new UserViewModel { Id = item.Id, Action = "", Name = item.Name });
                    }
                    var msgRequest = new RestRequest("log", Method.GET);
                    msgRequest.RequestFormat = RestSharp.DataFormat.Json;
                    var msgList = AppVariables.restClient.Execute<List<MessageModel>>(msgRequest);
                    AppVariables.Messages = new System.Collections.ObjectModel.ObservableCollection<MessageModel>(msgList.Data);
                    var main = (MainWindow)this.Parent;
                    main.Content = new pgChat();
                    main.ConnectToHub();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblError.Content = "There was an error when trying to connect to the service";
            }
        }
    }
}