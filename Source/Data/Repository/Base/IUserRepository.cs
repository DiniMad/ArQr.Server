using System.Threading.Tasks;
using Domain;

namespace Data.Repository.Base
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetAsync(string phoneNumber);
    }
}