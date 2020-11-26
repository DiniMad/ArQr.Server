using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Base;

namespace Data.Repository.Base
{
    public interface IRepository<TDomain, TKey> where TDomain : BaseDomain<TKey> where TKey : struct
    {
        Task<TDomain?>             GetAsync(long id);
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<IEnumerable<TDomain>> FindAsync(Expression<Func<TDomain, bool>>  predicate);
        Task                       InsertAsync(TDomain                        domain);
        Task                       InsertCollectionAsync(IEnumerable<TDomain> domains);
        void                       Update(TDomain                             domain);
        void                       UpdateCollection(IEnumerable<TDomain>      domains);
        void                       Remove(TDomain                             domain);
        void                       RemoveCollection(IEnumerable<TDomain>      domains);
    }
}