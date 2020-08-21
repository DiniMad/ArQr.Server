using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArQr.Data;
using Microsoft.EntityFrameworkCore;

namespace ArQr.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsByUserIdAsync(string ownerId, int take, int after)
        {
            return await _dbContext.Products
                                   .AsNoTracking()
                                   .Where(product => product.OwnerId == ownerId)
                                   .Take(take)
                                   .Skip(after)
                                   .ToListAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }
    }
}