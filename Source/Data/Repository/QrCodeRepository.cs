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
    public class QrCodeRepository : Repository<QrCode>, IQrCodeRepository
    {
        public QrCodeRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<QrCode>> FindAsync(Expression<Func<QrCode, bool>> predicate, int after, int take)
        {
            return await Context.Set<QrCode>().Where(predicate).Skip(after).Take(take).ToListAsync();
        }
    }
}