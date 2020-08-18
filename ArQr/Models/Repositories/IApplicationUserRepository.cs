using System.Threading.Tasks;

namespace ArQr.Models.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetUserAsync(string userId);
    }
}