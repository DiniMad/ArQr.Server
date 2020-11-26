using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class PurchaseRepository : Repository<Purchase, long>, IPurchaseRepository
    {
        public PurchaseRepository(DbContext context) : base(context)
        {
        }
    }
}