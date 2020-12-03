namespace Resource.Api.Resources
{
    public record QrCodeResource
    {
        public long    Id                    { get; set; }
        public string  Title                 { get; init; }
        public string  Description           { get; init; }
        public string? AssociatedPhoneNumber { get; init; }
        public string? AssociatedWebsite     { get; init; }
        public long?   MediaContentId        { get; init; }
    }

    public sealed record AuthorizeQrCodeResource : QrCodeResource
    {
        public int  CreationDate           { get; init; }
        public int  ExpireDate             { get; init; }
        public bool Expired                { get; init; }
        public int  ViewersCount           { get; set; }
        public int  MaxAllowedViewersCount { get; set; }
        public bool ReachedMaxViews        { get; set; }
    }


    public sealed record AddViewerResource(long ViewerId);

    public sealed record QrCodeViewersCountResource(int Count);

    public sealed record UpdateQrCodeResource(string? Title,
                                              string? Description,
                                              string? AssociatedPhoneNumber,
                                              string? AssociatedWebsite,
                                              long?   MediaContentId);
}