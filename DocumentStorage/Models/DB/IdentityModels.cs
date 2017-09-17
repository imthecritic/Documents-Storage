using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace DocumentStorage.Models.DB
{
    public class RoleClaim: IdentityRoleClaim<int>
    {
    }

    public class UserClaim : IdentityUserClaim<int>
    {
    }

    public class UserLogin : IdentityUserLogin<int>
    {
    }


    public class UserToken : IdentityUserToken<int>
    {
    }

    public class AppRole : IdentityRole<int>
    {

    }

    public class UserRole : IdentityRole<int>
    {
        public UserRole() { }
        public UserRole(string name) { Name = name; }
    }

    public class AppUserStore : UserStore<User, UserRole, AppDbContext, int>
    {
        public AppUserStore(AppDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

      
    }

    //public class AppRoleStore : RoleStore<AppRole, AppDbContext, int>,
    //    IQueryableRoleStore<AppRole>,
    //    IRoleStore<AppRole>,
    //    IDisposable,
    //    IRoleClaimStore<AppRole>

    //{
    //    public AppRoleStore(AppDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
    //    {
    //    }
    //}


}
