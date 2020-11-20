using System;
using System.Threading.Tasks;
using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<User?> GetIncludeRefreshTokenAsync(string phoneNumber)
        {
            return await Context.Set<User>()
                                .AsNoTracking()
                                .Include(user => user.RefreshToken)
                                .FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        }

        public async Task<User?> GetIncludeRefreshTokenAsync(Guid userId)
        {
            return await Context.Set<User>()
                                .AsNoTracking()
                                .Include(user => user.RefreshToken)
                                .FirstOrDefaultAsync(user => user.Id == userId);
        }
    }
}