using RestSharp;
using SimpleChatApp.Models;
using System;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = string.Empty;
            var client = new RestClient("http://localhost:59146/api");
            while (command.ToLowerInvariant() != "q")
            {
                Console.WriteLine("Say something:");
                command = Console.ReadLine();
                var msg = new MessageModel {
                    Message = command,
                    Name = "test user",
                    Posted = DateTime.Now,
                    UserId = Guid.NewGuid()
                };
                var request = new RestRequest("message", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddObject(msg);
                client.Execute(request);
            }
        }
    }
}
