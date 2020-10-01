using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Repositories
{
    public class ClientStore : IClientStore
    {
        private readonly DataContext _dataContext;

        public ClientStore(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = await _dataContext.FindAsync<Entities.Client>(clientId);

            if (client == null) return null;

            return new Client
            {
                ClientId = client.Id,

                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,

                RedirectUris = client.RedirectUris.Split(";;"),
                FrontChannelLogoutUri = client.FrontChannelLogoutUri,
                PostLogoutRedirectUris = client.PostLogoutRedirectUris.Split(";;"),
                AllowedCorsOrigins = client.AllowedCorsOrigins.Split(";;"),
                AllowedScopes = (new [] { "openid", "profile" }).Union(client.AllowedScopes.Split(";;")).ToList()
            };
        }
    }
}
