using System;

namespace Resource.Api.Resources
{
    public sealed record CreateUploadSessionResource(long MediaContentId, string Extension, byte TotalSizeInMb);

    public sealed record UploadChunkResource(long MediaContentId, byte ChunkNumber, byte[] Content);

    public sealed record UploadCompletedResource(Guid Session);
}