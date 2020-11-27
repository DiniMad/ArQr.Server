using System;

namespace Resource.Api.Resources
{
    public sealed record CreateUploadSessionResource(long MediaContentId, string Extension, byte TotalSizeInMb);

    public sealed record UploadSessionResource(Guid Session);
}