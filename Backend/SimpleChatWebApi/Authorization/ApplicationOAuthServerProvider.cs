using Microsoft.Owin.Security.OAuth;
using SimpleChatApp.Models;
using SimpleChatWebApi.Handlers;
using SimpleChatWebApi.Models;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleChatWebApi.Authorization
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (AppVariables.Users.Count >= 20)
            {
                context.SetError("limit_reached", "maximum 20 users allowed. cap reached.");
                return;
            }
            var item = AppVariables.Users.SingleOrDefault(p => p.User.Name.ToLowerInvariant() == context.UserName.ToLowerInvariant());
            if (item!=null)
            {
                context.SetError("invalid_grant", "user with same name already exists");
                return;
            }
            var user = new UserServerModel
            {
                User = new UserModel {
                    Id = context.UserName,
                    Name = context.UserName
                }
            };
            AppVariables.Users.Add(user);
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("user_name", context.UserName));
            context.Validated(identity);
        }

    }
}