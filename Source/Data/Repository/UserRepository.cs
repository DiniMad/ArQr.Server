using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UserRepository : Repository<User,long>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<User?> GetIncludeRefreshTokenAsync(string phoneNumber)
        {
            return await GetIncludeRefreshTokenAsync(user => user.PhoneNumber == phoneNumber);
        }

        public async Task<User?> GetIncludeRefreshTokenAsync(long userId)
        {
            return await GetIncludeRefreshTokenAsync(user => user.Id == userId);
        }

        private async Task<User?> GetIncludeRefreshTokenAsync(Expression<Func<User, bool>> predict)
        {
            return await Context.Set<User>()
                                .AsNoTracking()
                                .Include(user => user.RefreshToken)
                                .FirstOrDefaultAsync(predict);
        }
    }
}