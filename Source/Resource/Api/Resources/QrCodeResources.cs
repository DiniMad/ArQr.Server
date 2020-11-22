namespace Resource.Api.Resources
{
    public sealed record QrCodeResource
    {
        public string  Title                 { get; init; }
        public string  Description           { get; init; }
        public int     CreationDate          { get; init; }
        public int     ExpireDate            { get; init; }
        public string? AssociatedPhoneNumber { get; init; }
        public string? AssociatedWebsite     { get; init; }
        public bool    Expired               { get; init; }
        public long    OwnerId               { get; init; }
    }
}