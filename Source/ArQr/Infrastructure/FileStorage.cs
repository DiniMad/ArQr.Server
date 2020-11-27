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

        public async Task WriteFileAsync(string fileName, byte[] content, string directory, string? subDirectory = null)
        {
            var filePath = Path.Join(BasePath, directory, subDirectory, fileName);

            await using var file = File.OpenWrite(filePath);
            await file.WriteAsync(content);
        }

        public bool DirectoryExist(string directory, string? subDirectory = null)
        {
            var fullDirectory = Path.Join(BasePath, directory, subDirectory);
            return Directory.Exists(fullDirectory);
        }

        public void DeleteDirectory(string directory, string? subDirectory = null)
        {
            var fullDirectory = Path.Join(BasePath, directory, subDirectory);
            Directory.Delete(fullDirectory, true);
        }

        public void ReCreateDirectory(string directory, string? subDirectory = null)
        {
            var fullDirectory = Path.Join(BasePath, directory, subDirectory);
            Directory.Delete(fullDirectory, true);
            Directory.CreateDirectory(fullDirectory);
        }
    }
}