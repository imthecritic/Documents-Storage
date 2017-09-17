using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MediatR;


namespace DocumentStorage.Models.DB
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, UserRole>
    {
        public CustomClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<UserRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {

        }
        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            return await Task.Factory.StartNew(() =>
             {

                 var identity = new ClaimsIdentity(new List<Claim>(), "Custom");
                 identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                 var principle = new ClaimsPrincipal(identity);

                 return principle;
             });
        }

        public ClaimsPrincipal Create(User user)
        {

            var identity = new ClaimsIdentity(new List<Claim>(), "Custom");
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            var principle = new ClaimsPrincipal(identity);

            return principle;
        }
    }
}
