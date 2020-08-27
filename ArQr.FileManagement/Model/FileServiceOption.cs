using System.Collections.Generic;

namespace ArQr.FileManagement.Model
{
    public class FileServiceOption
    {
        private const string DefaultStoragePath = "./Storage";
        private const int    DefaultChunkSize   = 1024 * 1024; // 1MB

        public string                    StoragePath          { get; }
        public int                       ChunkSizeInByte      { get; }
        public IDictionary<string, long> FileSizeLimitInByte  { get; }
        public bool                      HasFileSizeLimit     => FileSizeLimitInByte != null;

        public FileServiceOption(string                    storagePath          = DefaultStoragePath,
                                  int                       chunkSizeInByte      = DefaultChunkSize,
                                  IDictionary<string, long> fileSizeLimitInByte  = null)
        {
            StoragePath          = storagePath;
            ChunkSizeInByte      = chunkSizeInByte;
            FileSizeLimitInByte  = fileSizeLimitInByte;
        }
    }
}