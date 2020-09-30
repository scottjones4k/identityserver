using IdentityModel;
using IdentityServer.Entities;
using IdentityServer.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IPasswordValidationService _passwordValidationService;

        public UserRepository(DataContext dataContext, IPasswordValidationService passwordValidationService)
        {
            _dataContext = dataContext;
            _passwordValidationService = passwordValidationService;

            /*if (!_dataContext.Users.Any())
            {
                _users.ForEach(u =>
                {
                    u.Password = _passwordValidationService.Encrypt(u.Username);
                    logger.LogDebug($"Password {u.Username}:{u.Password}");
                    logger.LogDebug($"Password Fine:{_passwordValidationService.IsMatch(u.Password, u.Username)}");
                    _dataContext.Users.Add(u);
                });
                _dataContext.SaveChanges();
            }*/
        }

        // some dummy data. Replce this with your user persistence. 
        /*private readonly List<User> _users = new List<User>
        {
            new User{
                Id = Guid.NewGuid(),
                Username = "alice",
                Password = "alice",
                Email = "damienbod@email.ch",
                Forename = "Alice",
                Surname = "Smith"
            },
            new User{
                Id = Guid.NewGuid(),
                Username = "raphael",
                Password = "raphael",
                Email = "raphael@email.ch",
                Forename = "Raphael",
                Surname = "Briggs"
            },
        };*/

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await FindByUsernameAsync(username);
            if (user != null)
            {
                return _passwordValidationService.IsMatch(user.Password, password);
            }

            return false;
        }

        public async Task<User> FindBySubjectIdAsync(string subjectId) =>
            await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse(subjectId));

        public async Task<User> FindByUsernameAsync(string username) =>
            await _dataContext.Users.FirstOrDefaultAsync(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        public async Task<User> FindByExternalProviderAsync(string provider, string providerUserId) =>
            await _dataContext.Users.FirstOrDefaultAsync(x => x.ExternalProvider == provider && x.ExternalProviderId == providerUserId);

        public async Task<User> AutoProvisionUserAsync(string provider, string providerUserId, List<Claim> claims)
        {
            var user = new User
            {
                Email = claims.FirstOrDefault(e => e.Type == JwtClaimTypes.Email || e.Type == ClaimTypes.Email).Value,
                ExternalProvider = provider,
                ExternalProviderId = providerUserId,
                Forename = claims.FirstOrDefault(e => e.Type == JwtClaimTypes.GivenName || e.Type == ClaimTypes.GivenName)?.Value,
                Surname = claims.FirstOrDefault(e => e.Type == JwtClaimTypes.FamilyName || e.Type == ClaimTypes.Surname)?.Value,
                Id = Guid.NewGuid(),
                Username = claims.FirstOrDefault(e => e.Type == JwtClaimTypes.PreferredUserName || e.Type == ClaimTypes.Email)?.Value,
            };
            await _dataContext.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
