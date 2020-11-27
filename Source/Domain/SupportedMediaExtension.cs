using Domain.Base;

namespace Domain
{
    public class SupportedMediaExtension : BaseDomain<byte>
    {
        public string Extension { get; set; }
    }
}