using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Base;

namespace Data.Repository.Base
{
    public interface IRepository<TDomain> where TDomain : BaseDomain
    {
        Task<TDomain>              GetAsync(Guid id);
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<IEnumerable<TDomain>> FindAsync(Expression<Func<TDomain, bool>>  predicate);
        Task                       InsertAsync(TDomain                        entity);
        Task                       InsertCollectionAsync(IEnumerable<TDomain> entities);
        void                       Remove(TDomain                             entity);
        void                       RemoveCollection(IEnumerable<TDomain>      entities);
    }
}