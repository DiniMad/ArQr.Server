using System;
using Domain.Base;

namespace Domain
{
    public class UserRefreshToken : BaseDomain
    {
        public string   Token      { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}