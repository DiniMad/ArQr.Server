using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArQr.Models.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsByUserIdAsync(string ownerId, int take, int after);
    }
}