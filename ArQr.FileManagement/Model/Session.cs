using System;
using System.Collections.Generic;

namespace ArQr.FileManagement.Model
{
    public class Session
    {
        public  string    Id                   { get; }
        public  string    FileExtension        { get; }
        public  string    FileType             { get; }
        public  long      FileSize             { get; }
        public  int       TotalChunks          { get; }
        private ISet<int> PersistedChunks      { get; }
        public  int       PersistedChunksCount => PersistedChunks.Count;

        public Session(string fileExtension, string fileType, long fileSize, int totalChunks)
        {
            Id              = Guid.NewGuid().ToString();
            FileExtension   = fileExtension;
            FileType        = fileType;
            FileSize        = fileSize;
            TotalChunks     = totalChunks;
            PersistedChunks = new HashSet<int>();
        }

        public void MarkChunkAsPersisted(int chunkNumber) => PersistedChunks.Add(chunkNumber);
    }
}