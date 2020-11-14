using Microsoft.EntityFrameworkCore;

namespace Data
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        {
        }
    }
}