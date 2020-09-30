using System;

namespace IdentityServer.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }

        public string ExternalProvider { get; set; }
        public string ExternalProviderId { get; set; }

        public string SubjectId => Id.ToString();
        public string FullName => $"{Forename} {Surname}";
    }
}
