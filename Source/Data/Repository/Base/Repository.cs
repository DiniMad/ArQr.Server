using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Base
{
    public abstract class Repository<TDomain, TKey> : IRepository<TDomain, TKey> where TDomain : BaseDomain<TKey>
        where TKey : struct
    {
        protected readonly DbContext Context;

        protected Repository(DbContext context)
        {
            Context = context;
        }

        public async Task<TDomain?> GetAsync(long id)
        {
            return await Context.Set<TDomain>()
                                .AsNoTracking()
                                .FirstOrDefaultAsync(domain => domain.Id.Equals(id));
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            return await Context.Set<TDomain>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TDomain>> FindAsync(Expression<Func<TDomain, bool>> predicate)
        {
            return await Context.Set<TDomain>().Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(TDomain domain)
        {
            await Context.Set<TDomain>().AddAsync(domain);
        }

        public async Task InsertCollectionAsync(IEnumerable<TDomain> domains)
        {
            await Context.Set<TDomain>().AddRangeAsync(domains);
        }

        public void Update(TDomain domain)
        {
            Context.Set<TDomain>().Update(domain);
        }

        public void UpdateCollection(IEnumerable<TDomain> domains)
        {
            Context.Set<TDomain>().UpdateRange(domains);
        }

        public void Remove(TDomain domain)
        {
            Context.Set<TDomain>().Remove(domain);
        }

        public void RemoveCollection(IEnumerable<TDomain> domains)
        {
            Context.Set<TDomain>().RemoveRange(domains);
        }
    }
}