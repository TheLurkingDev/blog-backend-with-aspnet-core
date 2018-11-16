using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {

        }
      
        public DbSet<Website> Website { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<BlogPost> BlogPost { get; set; }
    }
}
