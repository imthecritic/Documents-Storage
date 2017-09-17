using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentStorage.Models.DB
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider serviceProvider,
            ILogger<UserManager<User>> logger)
            : base(
                store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                serviceProvider, logger)
        {
        }
    }



    public class ApplicationSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationSignInManager(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger, AppDbContext dbContext)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            if (contextAccessor == null)
                throw new ArgumentNullException(nameof(contextAccessor));

            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _db = dbContext;
        }
    }


}
