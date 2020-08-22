using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArQr.Models.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity>              GetAsync(string id);
        Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity                   entity);
        Task AddRangeAsync(IList<TEntity> entities);

        void RemoveAsync(TEntity                   entity);
        void RemoveRangeAsync(IList<TEntity> entities);
    }
}