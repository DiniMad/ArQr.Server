using System;

namespace Domain
{
    public class UserRefreshToken
    {
        public string   Token      { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool     IsExpired  => DateTime.UtcNow > ExpireDate;
        public long     UserId     { get; set; }
    }
}