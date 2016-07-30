using RestSharp;
using RestSharp.Deserializers;
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
            var tokenServer = new RestClient($"{Properties.Settings.Default.ChatServiceUrl}");
            var request = new RestRequest("token", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", txtUsername.Text);
            request.AddParameter("password", "");
            request.AddBody(txtUsername.Text);
            try
            {
                var response = tokenServer.Execute<UserModel>(request);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = new JsonDeserializer().Deserialize<AuthorizationErrorModel>(response);
                    lblError.Content = error.ErrorDescription;
                    return;
                }else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AppVariables.Username = txtUsername.Text;
                    var token = new JsonDeserializer().Deserialize<AuthorizationTokenModel>(response);
                    AppVariables.Token = token.Token;
                    var main = (MainWindow)Parent;
                    main.ConnectToHub();
                    request = new RestRequest("users", Method.GET);
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Authorization", $"Bearer {AppVariables.Token}");
                    var userList = AppVariables.restClient.Execute<List<UserModel>>(request);
                    AppVariables.UserList = new System.Collections.ObjectModel.ObservableCollection<UserViewModel>();
                    
                    foreach (var item in userList.Data)
                    {
                        AppVariables.UserList.Add(new UserViewModel { Id = item.Id, Action = "", Name = item.Name });
                    }
                    var msgRequest = new RestRequest("log", Method.GET);
                    msgRequest.RequestFormat = RestSharp.DataFormat.Json;
                    msgRequest.AddHeader("Authorization", $"Bearer {AppVariables.Token}");
                    var msgList = AppVariables.restClient.Execute<List<MessageModel>>(msgRequest);
                    AppVariables.Messages = new System.Collections.ObjectModel.ObservableCollection<MessageModel>(msgList.Data);
                    main.Content = new pgChat();
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