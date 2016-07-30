using Microsoft.Owin.Security.OAuth;
using System;
using System.Threading.Tasks;

namespace SimpleChatWebApi.Handlers
{
    public class OAuthBearerTokenAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            string headerToken = null;

            try
            {
                headerToken = context.OwinContext.Request.Headers["BearerToken"];
            }
            catch (NullReferenceException)
            {
                System.Diagnostics.Debug.WriteLine("The connection header does not contain the bearer token");
            }

            if (!string.IsNullOrEmpty(headerToken)) context.Token = headerToken;

            return Task.FromResult<object>(null);
        }
    }
}