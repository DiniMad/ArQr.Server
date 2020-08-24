using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArQr.Data;
using Microsoft.EntityFrameworkCore;

namespace ArQr.Models.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext DbContext => Context as ApplicationDbContext;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Product>> GetProductsByUserIdAsync(string ownerId, int take, int after)
        {
            return await DbContext.Products
                                  .AsNoTracking()
                                  .Where(product => product.OwnerId == ownerId)
                                  .Skip(after)
                                  .Take(take)
                                  .ToListAsync();
        }
    }
}