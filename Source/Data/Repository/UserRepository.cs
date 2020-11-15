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
    }
}