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

        public async Task WriteFromDiskToStream(string directory, string fileName, Stream stream)
        {
            var fullFileName = Path.Join(BasePath, directory, fileName);
            await foreach (var bytes in ReadAsyncEnumerable(fullFileName)) await stream.WriteAsync(bytes);
        }

        private static async IAsyncEnumerable<byte[]> ReadAsyncEnumerable(string path)
        {
            const int       chunkSize = 1024 * 1024; // read the file by chunks of 1MB
            var             buffer    = new byte[chunkSize];
            await using var file      = File.OpenRead(path);
            while (await file.ReadAsync(buffer.AsMemory(0, buffer.Length)) > 0) yield return buffer;
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