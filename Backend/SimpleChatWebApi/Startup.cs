using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleChatWebApi.Authorization;
using System;

[assembly: OwinStartup(typeof(SimpleChatWebApi.Startup))]
namespace SimpleChatWebApi
{
    /// <summary>
    /// Startup class of Simple Chat WebAPi
    /// </summary>
    public class Startup
    {
        public static OAuthAuthorizationServerOptions AuthServerOptions;
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "Application",
            //    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive,
            //    LoginPath = "/api/login"
            //});
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
            app.MapSignalR();
            GlobalHost.HubPipeline.RequireAuthentication();
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}