using Domain.Base;

namespace Domain
{
    public class QrCodeViewer : BaseDomain<long>
    {
        public long QrCodeId { get; set; }
    }
}