using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class SupportedMediaExtensionRepository : Repository<SupportedMediaExtension, byte>,
                                                     ISupportedMediaExtensionRepository
    {
        public SupportedMediaExtensionRepository(DbContext context) : base(context)
        {
        }
    }
}