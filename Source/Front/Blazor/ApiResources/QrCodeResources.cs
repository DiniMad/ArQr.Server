namespace Blazor.ApiResources
{
    public record QrCodeResource
    {
        public long    Id                    { get; set; }
        public string  Title                 { get; set; }
        public string  Description           { get; set; }
        public string? AssociatedPhoneNumber { get; set; }
        public string? AssociatedWebsite     { get; set; }
        public long?   MediaContentId        { get; set; }
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