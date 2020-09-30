using IdentityModel;
using IdentityServer.Repositories;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        protected readonly ILogger Logger;


        protected readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository, ILogger<ProfileService> logger)
        {
            _userRepository = userRepository;
            Logger = logger;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            Logger.LogDebug("Get profile called for subject {subject} from client {client} with claim types {claimTypes} via {caller}",
                sub,
                context.Client.ClientName ?? context.Client.ClientId,
                context.RequestedClaimTypes,
                context.Caller);

            var user = await _userRepository.FindBySubjectIdAsync(sub);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, $"{user.Forename} {user.Surname}"),
                new Claim(JwtClaimTypes.GivenName, user.Forename),
                new Claim(JwtClaimTypes.FamilyName, user.Surname),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userRepository.FindBySubjectIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
