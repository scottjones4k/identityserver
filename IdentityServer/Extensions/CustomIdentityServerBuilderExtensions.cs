using IdentityServer.Repositories;
using IdentityServer.Services;
using IdentityServer.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Extensions
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IPasswordValidationService, PasswordValidationService>();
            builder.AddProfileService<ProfileService>();
            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
