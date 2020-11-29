using System;

namespace Resource.Api.Resources
{
    public sealed record CreateUploadSessionResource(long MediaContentId, string Extension, byte TotalSizeInMb);

    public sealed record UploadSessionResource(Guid Session);

    public sealed record UploadChunkResource(Guid Session, byte ChunkNumber, byte[] Content);

    public sealed record UploadCompletedResource(Guid Session);
}