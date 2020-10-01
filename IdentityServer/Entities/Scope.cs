using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Entities
{
    public class Scope
    {
        [MaxLength(50)]
        public string Id { get; set; }
    }
}
