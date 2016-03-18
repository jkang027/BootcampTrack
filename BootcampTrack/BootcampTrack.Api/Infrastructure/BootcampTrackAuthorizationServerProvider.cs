using BootcampTrack.Core.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BootcampTrack.Api.Infrastructure
{
    public class BootcampTrackAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private Func<IAuthorizationRepository> _authRepositoryFactory;

        private IAuthorizationRepository _authRepository => _authRepositoryFactory.Invoke();

        public BootcampTrackAuthorizationServerProvider(Func<IAuthorizationRepository> authRepositoryFactory)
        {
            _authRepositoryFactory = authRepositoryFactory;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                context.Validated();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Cors
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Validate the User
            var user = await _authRepository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");

                return;
            }

            var token = new ClaimsIdentity(context.Options.AuthenticationType);

            token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            foreach(var role in user.Roles)
            {
                token.AddClaim(new Claim(ClaimTypes.Role, role.Role.Name));
            }

            context.Validated(token);
        }
    }
}