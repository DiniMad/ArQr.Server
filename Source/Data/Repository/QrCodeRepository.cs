using Data.Repository.Base;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class QrCodeRepository: Repository<QrCode>,IQrCodeRepository
    {
        public QrCodeRepository(DbContext context) : base(context)
        {
        }
    }
}