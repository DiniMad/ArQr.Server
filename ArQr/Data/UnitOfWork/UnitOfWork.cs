using System.Threading.Tasks;
using ArQr.Models.Repositories;

namespace ArQr.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext       _dbContext;
        public           IApplicationUserRepository Users    { get; }
        public           IProductRepository         Products { get; }

        public UnitOfWork(ApplicationDbContext dbContext, IApplicationUserRepository users, IProductRepository products)
        {
            _dbContext = dbContext;
            Users      = users;
            Products   = products;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}