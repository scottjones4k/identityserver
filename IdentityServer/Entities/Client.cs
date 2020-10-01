using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Entities
{
    public class Client
    {
        [MaxLength(50)]
        public string Id { get; set; }
        public string RedirectUris { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public string PostLogoutRedirectUris { get; set; }
        public string AllowedCorsOrigins { get; set; }
        public string AllowedScopes { get; set; }
    }
}
