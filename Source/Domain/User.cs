using System.Collections.Generic;
using Domain.Base;

namespace Domain
{
    public class User : BaseDomain
    {
        public string              PhoneNumber          { get; set; }
        public string              PasswordHash         { get; set; }
        public string              Email                { get; set; }
        public bool                EmailConfirmed       { get; set; }
        public bool                PhoneNumberConfirmed { get; set; }
        public UserRefreshToken    RefreshToken         { get; set; }
        public IEnumerable<QrCode> QrCodes              { get; set; }
    }
}