using System.Threading.Tasks;
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

        public async Task<SupportedMediaExtension?> GetAsync(string extension)
        {
            return await Context.Set<SupportedMediaExtension>()
                                .AsNoTracking()
                                .FirstOrDefaultAsync(mediaExtension => mediaExtension.Extension == extension);
        }
    }
}