using Domain.Base;

namespace Domain
{
    public class Service : BaseDomain<byte>
    {
        public string Title                    { get; set; }
        public string Description              { get; set; }
        public int    UnitPriceInThousandToman { get; set; }
        public bool   Active                   { get; set; }
    }
}