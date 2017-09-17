using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DocumentStorage.Models.DB
{
    public interface IAppUserStore
    {
        IQueryable<User> Users { get; }

        Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken));
        Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken));
        Task AddToRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken));
        string ConvertIdFromString(string id);
        string ConvertIdToString(string id);
        Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        bool Equals(object obj);
        Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken));
        Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken));
        Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));
        Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        int GetHashCode();
        Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<User>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> IsInRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveFromRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken));
        Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken));
        Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
        Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default(CancellationToken));
        Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = default(CancellationToken));
        Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken = default(CancellationToken));
        Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken = default(CancellationToken));
        Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken));
        Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken));
        Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default(CancellationToken));
        Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));
        Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = default(CancellationToken));
        Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken = default(CancellationToken));
        Task SetTokenAsync(User user, string loginProvider, string name, string value, CancellationToken cancellationToken);
        Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken = default(CancellationToken));
        Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default(CancellationToken));
        string ToString();
        Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
    }
}