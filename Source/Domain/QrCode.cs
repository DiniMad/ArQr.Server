using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain
{
    public class QrCode : BaseDomain<long>
    {
        public string                    Title                  { get; set; }
        public string                    Description            { get; set; }
        public int                       CreationDate           { get; set; }
        public int                       ExpireDate             { get; set; }
        public string?                   AssociatedPhoneNumber  { get; set; }
        public string?                   AssociatedWebsite      { get; set; }
        public int                       ViewersCount           { get; set; }
        public int                       MaxAllowedViewersCount { get; set; }
        public User                      Owner                  { get; set; }
        public long                      OwnerId                { get; set; }
        public IEnumerable<QrCodeViewer> Viewers                { get; set; }

        public QrCode()
        {
            Viewers = new List<QrCodeViewer>();
        }
    }
}