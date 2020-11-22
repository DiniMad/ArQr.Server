using System;
using System.Threading.Tasks;
using Domain;

namespace Data.Repository.Base
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetIncludeRefreshTokenAsync(string phoneNumber);
        Task<User?> GetIncludeRefreshTokenAsync(long userId);
    }
}