using IdentityServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ValidateCredentialsAsync(string username, string password);

        Task<User> FindBySubjectIdAsync(string subjectId);

        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByExternalProviderAsync(string provider, string providerUserId);
        Task<User> AutoProvisionUserAsync(string provider, string providerUserId, List<Claim> lists);
    }
}
