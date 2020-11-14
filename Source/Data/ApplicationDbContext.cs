using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        {
        }
    }
}