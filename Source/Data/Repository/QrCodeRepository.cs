using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class QrCodeRepository : Repository<QrCode,long>, IQrCodeRepository
    {
        public QrCodeRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<QrCode>> FindAsync(Expression<Func<QrCode, bool>> predicate, int after, int take)
        {
            return await Context.Set<QrCode>().AsNoTracking().Where(predicate).Skip(after).Take(take).ToListAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<QrCode, bool>> predicate)
        {
            return await Context.Set<QrCode>().AsNoTracking().Where(predicate).CountAsync();
        }
    }
}