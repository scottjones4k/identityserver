using IdentityModel;
using IdentityServer.Repositories;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Validators
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (await _userRepository.ValidateCredentialsAsync(context.UserName, context.Password))
            {
                var user = await _userRepository.FindByUsernameAsync(context.UserName);
                context.Result = new GrantValidationResult(user.SubjectId, OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}
