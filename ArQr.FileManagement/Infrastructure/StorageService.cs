using System;
using System.IO;
using System.Threading.Tasks;
using ArQr.FileManagement.Model;

namespace ArQr.FileManagement.Infrastructure
{
    public static class StorageService
    {
        private static string StoragePath { get; set; }
        private static bool   Initialized { get; set; }

        public static void Initial(string storagePath)
        {
            StoragePath = storagePath;
            Initialized = true;
        }

        public static async Task WriteToDiskAsync(string sessionId, int chunkNumber, byte[] buffer)
        {
            if (!Initialized) ThrowNotInitializeError();

            var sessionPath = Path.Combine(StoragePath, sessionId);

            if (!Directory.Exists(sessionPath)) Directory.CreateDirectory(sessionPath);

            var chunkPath = Path.Combine(sessionPath, chunkNumber.ToString());
            await File.WriteAllBytesAsync(chunkPath, buffer);
        }

        public static async Task WriteToStreamAsync(Stream stream, Session session)
        {
            if (!Initialized) ThrowNotInitializeError();

            var path = Path.Combine(StoragePath, session.Id);

            for (var i = 1; i <= session.TotalChunks; i++)
                await stream.WriteAsync(await ReadFromDiskAsync(Path.Combine(path, i.ToString())));

            await stream.FlushAsync();
        }

        private static Task<byte[]> ReadFromDiskAsync(string path) => File.ReadAllBytesAsync(path);

        private static void ThrowNotInitializeError()
            => throw new
                   Exception($"{nameof(StorageService)} not been initialized. Use the {nameof(Initial)} method first.");
    }
}