using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ArQr.Helper;
using ArQr.Interface;
using Microsoft.Extensions.Configuration;

namespace ArQr.Infrastructure
{
    public class FileStorage : IFileStorage
    {
        public string BasePath { get; }

        public FileStorage(IConfiguration configuration)
        {
            BasePath = configuration.GetFileStoragePath();
            var baseDirectoryExist = Directory.Exists(BasePath);
            if (baseDirectoryExist is false) Directory.CreateDirectory(BasePath);
        }

        public async Task WriteFileAsync(string directory, string fileName, byte[] content)
        {
            var filePath = Path.Join(BasePath, directory, fileName);

            await using var stream = new FileStream(filePath, FileMode.Append);
            await stream.WriteAsync(content);
        }

        public async Task WriteFromDiskToStream(string directory, string fileName, Stream stream, int chunkSize)
        {
            var fullFileName = Path.Join(BasePath, directory, fileName);
            await foreach (var bytes in ReadAsyncEnumerable(fullFileName, chunkSize)) await stream.WriteAsync(bytes);
        }

        private async IAsyncEnumerable<byte[]> ReadAsyncEnumerable(string path, int chunkSize)
        {
            var             buffer = new byte[chunkSize];
            await using var file   = File.OpenRead(path);
            int             readBytesCount;
            while ((readBytesCount = await file.ReadAsync(buffer.AsMemory(0, buffer.Length))) > 0)
                yield return buffer[..readBytesCount];
        }

        public long GetFileSize(string directory, string fileName)
        {
            var      fullFileName = Path.Join(BasePath, directory, fileName);
            FileInfo file         = new(fullFileName);
            return file.Length;
        }

        public bool DirectoryExist(string directory)
        {
            var fullDirectory = Path.Join(BasePath, directory);
            return Directory.Exists(fullDirectory);
        }

        public void DeleteDirectory(string directory)
        {
            var fullDirectory = Path.Join(BasePath, directory);
            Directory.Delete(fullDirectory, true);
        }

        public void ReCreateDirectory(string directory)
        {
            var           fullDirectory = Path.Join(BasePath, directory);
            DirectoryInfo directoryInfo = new(fullDirectory);
            if (directoryInfo.Exists is true) directoryInfo.Delete(true);
            Directory.CreateDirectory(fullDirectory);
        }
    }
}