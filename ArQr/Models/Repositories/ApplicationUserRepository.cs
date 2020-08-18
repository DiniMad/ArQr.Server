using System.Threading.Tasks;
using ArQr.Data;
using Microsoft.EntityFrameworkCore;

namespace ArQr.Models.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await _dbContext.Users
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(user => user.Id == userId);
        }
    }
}