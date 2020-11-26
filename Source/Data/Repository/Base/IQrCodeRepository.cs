using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;

namespace Data.Repository.Base
{
    public interface IQrCodeRepository : IRepository<QrCode,long>
    {
        Task<IEnumerable<QrCode>> FindAsync(Expression<Func<QrCode, bool>>     predicate, int after, int take);
        Task<int>                 GetCountAsync(Expression<Func<QrCode, bool>> predicate);
    }
}