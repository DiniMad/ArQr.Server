namespace ArQr.Models
{
    public record FileChunksOptions
    {
        private const int ByteCoefficient = 1024;
        private       int UploadChunkSizeInKb   { get; init; }
        private       int DownloadChunkSizeInKb { get; init; }
        public        int UploadChunkSize       => UploadChunkSizeInKb   * ByteCoefficient;
        public        int DownloadChunkSize     => DownloadChunkSizeInKb * ByteCoefficient;
    }
}