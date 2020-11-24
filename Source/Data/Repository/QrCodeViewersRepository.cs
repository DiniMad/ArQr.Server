using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class QrCodeViewersRepository : Repository<QrCodeViewer>, IQrCodeViewersRepository
    {
        public QrCodeViewersRepository(DbContext context) : base(context)
        {
        }
    }
}