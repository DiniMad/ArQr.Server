using Domain.Base;

namespace Domain
{
    public class MediaContent : BaseDomain<long>
    {
        public string Extension     { get; set; }
        public bool   Verified      { get; set; }
        public byte   TotalSizeInMb { get; set; }
        public int    CreationDate  { get; set; }
        public int    ExpireDate    { get; set; }
        public long   UserId        { get; set; }
        public User   User          { get; set; }
    }
}