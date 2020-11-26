using Domain.Base;

namespace Domain
{
    public class Purchase : BaseDomain<long>
    {
        public string   TransactionCode           { get; set; }
        public string   GatewayName               { get; set; }
        public byte     Quantity                  { get; set; }
        public int      Date                      { get; set; }
        public int      PaidAmountInThousandToman { get; set; }
        public byte     OffAmountInThousandToman  { get; set; }
        public long     UserId                    { get; set; }
        public User?    User                      { get; set; }
        public byte     ServiceId                 { get; set; }
        public Service? Service                   { get; set; }
    }
}