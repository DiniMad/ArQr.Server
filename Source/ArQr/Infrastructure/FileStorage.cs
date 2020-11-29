using System.IO;
using System.Linq;
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

            await using var file = File.OpenWrite(filePath);
            await file.WriteAsync(content);
        }

        public async Task WriteChunksFromDiskToStream(string directory, Stream stream)
        {
            var fullDirectory = Path.Join(BasePath, directory);
            var filesCount    = Directory.EnumerateFiles(fullDirectory).Count();

            for (var i = 0; i < filesCount; i++)
            {
                var fileDirectory = Path.Join(fullDirectory, i.ToString());
                var bytes         = await File.ReadAllBytesAsync(fileDirectory);
                await stream.WriteAsync(bytes);
            }
        }

        public long CalculateChunksTotalSize(string directory)
        {
            var fullDirectory = Path.Join(BasePath, directory);
            var filesCount    = Directory.EnumerateFiles(fullDirectory).Count();
            if (filesCount == 0) return 0;
            
            var      firstFilePath = Path.Join(fullDirectory, "1");
            FileInfo firstFile     = new(firstFilePath);

            var      lastFilePath = Path.Join(fullDirectory, (filesCount - 1).ToString());
            FileInfo lastFile     = new(lastFilePath);

            var totalLength = firstFile.Length * (filesCount - 1) + lastFile.Length;

            return totalLength;
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