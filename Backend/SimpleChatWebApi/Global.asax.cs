using SimpleChatApp.Models;
using SimpleChatWebApi.Data;
using SimpleChatWebApi.Handlers;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SimpleChatWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public override void Init()
        {
            base.Init();
            this.AcquireRequestState += showRouteValues;
        }

        protected void showRouteValues(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context == null)
                return;
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(context));
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            using (var context = new ChatDbContext())
            {
                var msgCount = context.Messages.Count();
                if (msgCount > 20)
                {
                    var minId = context.Messages.OrderByDescending(p => p.DatePosted).Take(20).Min(p => p.Id);
                    var messages = context.Messages.Where(p => p.Id < minId);
                    context.Messages.RemoveRange(messages);
                    context.SaveChanges();
                }
                var log = context.Messages.OrderBy(p => p.Id);
                AppVariables.Messages.AddRange(
                    log.Select(l => new MessageModel
                    {
                        Message = l.Message,
                        Name = l.UserName,
                        Posted = l.DatePosted,
                        UserId = l.UserId
                    }));
            }
        }
    }
}