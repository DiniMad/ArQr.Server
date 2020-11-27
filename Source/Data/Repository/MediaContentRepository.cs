using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class MediaContentRepository : Repository<MediaContent, long>, IMediaContentRepository
    {
        public MediaContentRepository(DbContext context) : base(context)
        {
        }
    }
}