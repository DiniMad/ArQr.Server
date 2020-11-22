using System;
using Domain.Base;

namespace Domain
{
    public class QrCode : BaseDomain
    {
        public string  Title                 { get; set; }
        public string  Description           { get; set; }
        public int     CreationDate          { get; set; }
        public int     ExpireDate            { get; set; }
        public string? AssociatedPhoneNumber { get; set; }
        public string? AssociatedWebsite     { get; set; }
        public bool    Expired               => ExpireDate > DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        public User    Owner                 { get; set; }
        public long    OwnerId               { get; set; }
    }
}