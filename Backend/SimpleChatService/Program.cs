using Microsoft.Owin.Hosting;
using System;

namespace SimpleChatService
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = Properties.Settings.Default.ServiceUrl;
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.WriteLine("Press [Enter] to quit the service");
                Console.ReadLine();
            }
        }
    }
}
