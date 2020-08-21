using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArQr.Models.Repositories
{
    public interface IProductRepository
    {
        Task<Product>                GetProductAsync(string          id);
        Task<IReadOnlyList<Product>> GetProductsByUserIdAsync(string ownerId, int take, int after);
        Task                         CreateAsync(Product             product);
    }
}