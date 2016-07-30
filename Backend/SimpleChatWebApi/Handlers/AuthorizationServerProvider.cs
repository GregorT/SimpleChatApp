using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleChatWebApi.Handlers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            string userName = null;

            var existing = AppVariables.Users.SingleOrDefault(p => p.User.Name == context.UserName);
            if (existing == null)
            {
                context.SetError("invalid_grant", "Incorrect user name or password");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("Role", "User"));
            identity.AddClaim(new Claim("UserName", userName));
            context.Validated(identity);
        }
    }
}