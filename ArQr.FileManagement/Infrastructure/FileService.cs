using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ArQr.FileManagement.Model;

namespace ArQr.FileManagement.Infrastructure
{
    public class FileService
    {
        public  FileServiceOption           Option   { get; }
        private Dictionary<string, Session> Sessions { get; }

        public FileService(FileServiceOption option)
        {
            Option = option;
            StorageService.Initial(option.StoragePath);
            Sessions = new Dictionary<string, Session>();
        }

        public Session CreateSession(string fileExtension, string fileType, long fileSize)
        {
            var totalChunks = (int) Math.Ceiling((fileSize + 1D) / Option.ChunkSizeInByte);
            var session     = new Session(fileExtension, fileType, fileSize, totalChunks);
            Sessions.Add(session.Id, session);

            return session;
        }

        public Session GetSession(string id) => Sessions[id];

        public async Task PersistBlockAsync(string sessionId, int chunkNumber, byte[] buffer)
        {
            var session = GetSession(sessionId);

            if (session == null) throw new ArgumentException("Session not found");

            await StorageService.WriteToDiskAsync(sessionId, chunkNumber, buffer);

            session.MarkChunkAsPersisted(chunkNumber);
        }

        public async Task WriteToStreamAsync(Stream stream, Session session)
            => await StorageService.WriteToStreamAsync(stream, session);

        public void RemoveSession(string sessionId)
        {
            Sessions.Remove(sessionId);
        }
    }
}