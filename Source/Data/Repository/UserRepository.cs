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

        public async Task<User?> GetAsync(string phoneNumber)
        {
            return await Context.Set<User>()
                                .AsNoTracking()
                                .FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        }
    }
}