using IdentityServer.Repositories;
using IdentityServer.Services;
using IdentityServer.Validators;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class CustomIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddSingleton<IPasswordValidationService, PasswordValidationService>();
            builder.AddProfileService<ProfileService>();
            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}
