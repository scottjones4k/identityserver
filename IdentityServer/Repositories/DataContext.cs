using IdentityServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
